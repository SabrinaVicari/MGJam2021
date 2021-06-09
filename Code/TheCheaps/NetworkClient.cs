﻿
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using TheCheapsLib;
using TheCheapsServer;

namespace TheCheaps
{
    public class NetworkClient
    {
        private NetPeerConfiguration config;
        private NetClient client;
        private NetConnection connection;

        private GameInput input;
        public GameSimulation simulation;
        public GameNetwork network;

        public event EventHandler StateChanged;
        private void OnStateChanged()
        {
            if (StateChanged != null)
                StateChanged.Invoke(this, null);
        }
        public int PlayerIndex { get { return Array.FindIndex(network.model.players,x => x!=null && x.Unique == NetworkManager.ThisPeerUnique); } }

        public NetworkClient(IPAddress ip, int port)
        {
            config = new NetPeerConfiguration("TheCheaps");
            client = new NetClient(config);
            client.Start();
            var string_ip = ip.ToString();
            connection = client.Connect(host: string_ip, port: port);            
            this.simulation = new GameSimulation();
            this.network = new GameNetwork();
            this.input = new GameInput(simulation.model);
            SendOp(NetworkOp.OpType.HandShake, NetworkManager.ThisPeerUnique, NetworkManager.ThisPlayerName);
        }
        public void Update(GameTime gameTime)
        {
            ProcessIncomingMessages();
            switch (network.model.serverState.GamePhase)
            {
                case NetworkServerState.Phase.Unset:
                    break;
                case NetworkServerState.Phase.Gameplay:
                    SendMessage(ClientMessageType.ActionState, this.input.getActionState());
                    input.update_times();
                    break;
                case NetworkServerState.Phase.Lobby:
                    break;
            }
        }
        private void ProcessIncomingMessages()
        {
            NetIncomingMessage msg = client.ReadMessage();
            while (msg != null)
            {
#if ECHO
                Console.WriteLine($"Message received {msg}");
#endif
                switch (msg.MessageType)
                {

                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        Console.WriteLine(msg.ReadString());
                        break;
                    case NetIncomingMessageType.Data:
#if VERBOSE
                        System.Diagnostics.Debug.WriteLine($"{msg} received from server");
#endif
                        var type = (NetworkServer.MessageType)msg.ReadByte();
                        switch (type)
                        {
                            case NetworkServer.MessageType.PeerState:
                                network.SetState(msg.Deserialize<NetworkState>());
                                break;
                            case NetworkServer.MessageType.SimulationState:
                                simulation.SetState(msg.Deserialize<SimulationState>());
                                break;
                            default:
                                throw new Exception("Invalid message type");
                        }
                        break;
                    default:
                        break;

                }
                client.Recycle(msg);
                msg = client.ReadMessage();
            }
        }

        internal void Dispose() { }

        private void SendOp(NetworkOp.OpType type, params object[] pars)
        {
            SendMessage(ClientMessageType.NetworkOp, new NetworkOp(type, pars), NetDeliveryMethod.ReliableOrdered);
        }
        private void SendMessage(ClientMessageType messageType, IBinarizable state, NetDeliveryMethod deliveryMethod = NetDeliveryMethod.UnreliableSequenced)
        {
            NetOutgoingMessage msg = client.CreateMessage();
            byte[] array = null;
            using (var memstream = new MemoryStream(128 * 1024))
            {
                using (var bw = new BinaryWriter(memstream))
                {
                    state.BinaryWrite(bw);
                }
                array = memstream.ToArray();
            }
            msg.Write((byte)messageType);
            msg.Write(array.Length);
            msg.Write(array);
            client.SendMessage(msg, deliveryMethod);
#if VERBOSE
            System.Diagnostics.Debug.WriteLine("Sent {bytes.Length} bytes to server with GamepadState");
#endif
        }

        internal void SetReady(bool ready)
        {
            SendMessage(ClientMessageType.NetworkOp, new NetworkOp(NetworkOp.OpType.SetReady, ready), NetDeliveryMethod.ReliableOrdered);
        }

        internal bool GetReady(int playerIndex)
        {
            return network.model.players[playerIndex].Ready;
        }

        internal void Disconnect()
        {
            SendOp(NetworkOp.OpType.Disconnect);
            client.Disconnect("DisconnectRequestedByUser");
        }
    }
}