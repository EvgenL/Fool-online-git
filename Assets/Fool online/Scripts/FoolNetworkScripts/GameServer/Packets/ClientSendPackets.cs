﻿using System;
using Evgen.Byffer;
using Fool_online.Scripts.FoolNetworkScripts.NetworksObserver;

namespace Fool_online.Scripts.FoolNetworkScripts
{
    public static class ClientSendPackets
    {
        /// <summary>
        /// Packet id's. Gets converted to long and send at beginning of each packet
        /// Ctrl+C, Ctrl+V between ServerHandlePackets on server and ClientSendPackets on client
        /// </summary>
        private enum ClientPacketId
        {
            //LOGIN
            Authorize = 1,

            //ROOMS
            CreateRoom,
            RefreshRoomList,
            JoinRoom,
            JoinRandom,
            GiveUp, // todo not used
            LeaveRoom,
            GetReady,
            GetNotReady,

            //GAMEPLAY
            DropCardOnTable,
            Pass,
            CoverCardOnTable,

            // ACCOUNT
            WithdrawFunds,
            UloadAvatar
        }


        /// <summary>
        /// Sends byte array to server.
        /// </summary>
        /// <param name="data">bytes to send</param>
        private static void SendDataToServer(byte[] data)
        {
            // Add data length at the beginning and send
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteLong(data.Length);
            buffer.WriteBytes(data);

            FoolWebClient.WriteToServer(buffer.ToArray());
        }

        /// <summary>
        /// Sends message containing only packet id
        /// </summary>
        private static void SendOnlyPacketId(ClientPacketId packetId)
        {
            ByteBuffer buffer = new ByteBuffer();

            buffer.WriteLong((long)packetId);

            SendDataToServer(buffer.ToArray());
        }

        /// <summary>
        /// Sends user account to gameserver on login.
        /// </summary>
        public static void Send_Authorize(string token)
        {
            ByteBuffer buffer = new ByteBuffer();

            buffer.WriteLong((long)ClientPacketId.Authorize); //=1
            buffer.WriteString(token);

            SendDataToServer(buffer.ToArray());
        }

        /// <summary>
        /// Sent when cover some card on table
        /// </summary>
        public static void Send_CreateRoom(int maxPlayers, int deckSize, double bet)
        {
            ByteBuffer buffer = new ByteBuffer();

            //Write packet id
            buffer.WriteLong((long)ClientPacketId.CreateRoom);

            //write max players
            buffer.WriteInteger(maxPlayers);
            //write deck size
            buffer.WriteInteger(deckSize);
            //write bet
            buffer.WriteDouble(bet);

            SendDataToServer(buffer.ToArray());
        }

        /// <summary>
        /// Sent when cover some card on table
        /// </summary>
        public static void Send_RefreshRoomList()
        {
            SendOnlyPacketId(ClientPacketId.RefreshRoomList);
        }

        /// <summary>
        /// I want to join any room without parameters
        /// </summary>
        public static void Send_JoinRoom(long roomId)
        {
            ByteBuffer buffer = new ByteBuffer();

            //Write packet id
            buffer.WriteLong((long)ClientPacketId.JoinRoom);

            //write room id
            buffer.WriteLong(roomId);

            SendDataToServer(buffer.ToArray());
        }

        /// <summary>
        /// I want to join any room without parameters
        /// </summary>
        public static void Send_JoinRandom()
        {
            SendOnlyPacketId(ClientPacketId.JoinRandom);
        }

        /// <summary>
        /// I want to leave or give up a game
        /// </summary>
        public static void Send_LeaveRoom()
        {
            SendOnlyPacketId(ClientPacketId.LeaveRoom);
        }

        /// <summary>
        /// Sent when i click 'ready' button in room
        /// </summary>
        public static void Send_GetReady()
        {
            SendOnlyPacketId(ClientPacketId.GetReady);
        }

        /// <summary>
        /// Sent when i click 'ready' button in room
        /// </summary>
        public static void Send_GetNotReady()
        {
            SendOnlyPacketId(ClientPacketId.GetNotReady);
        }

        /// <summary>
        /// Sent when i click drop card on table
        /// </summary>
        public static void Send_DropCardOnTable(string cardCode)
        {
            ByteBuffer buffer = new ByteBuffer();

            //Add packet id
            buffer.WriteLong((long)ClientPacketId.DropCardOnTable);
            //Add card
            buffer.WriteString(cardCode);

            SendDataToServer(buffer.ToArray());
        }

        /// <summary>
        /// Sent when i click pass button in room
        /// </summary>
        public static void Send_Pass()
        {
            SendOnlyPacketId(ClientPacketId.Pass);
        }

        /// <summary>
        /// Sent when cover some card on table
        /// </summary>
        public static void Send_CoverCardOnTable(string cardCodeOnTable, string cardCodeDropped)
        {
            ByteBuffer buffer = new ByteBuffer();

            //Write packet id
            buffer.WriteLong((long)ClientPacketId.CoverCardOnTable);

            //write card on table
            buffer.WriteString(cardCodeOnTable);
            //write card dropped
            buffer.WriteString(cardCodeDropped);

            SendDataToServer(buffer.ToArray());
        }

        public static void Send_WithdrawFunds(float sum, string requisites)
        {
            ByteBuffer buffer = new ByteBuffer();

            //Write packet id
            buffer.WriteLong((long)ClientPacketId.WithdrawFunds);

            // withdraw sum
            buffer.WriteFloat(sum);
            buffer.WriteString(requisites);

            SendDataToServer(buffer.ToArray());
        }

        public static void Send_UploadAvatar(byte[] imageBytes)
        {
            ByteBuffer buffer = new ByteBuffer();

            //Write packet id
            buffer.WriteLong((long)ClientPacketId.UloadAvatar);

            // avatar binary data
            buffer.WriteBytes(imageBytes);

            SendDataToServer(buffer.ToArray());
        }
        
    }
}
