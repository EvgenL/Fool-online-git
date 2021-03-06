﻿using System.Collections.Generic;
using Fool_online.Scripts.FoolNetworkScripts;
using UnityEngine;

namespace Fool_online.Scripts.InRoom
{
    public class PlayerInRoom : FoolPlayer
    {
        public PlayerInRoom(long connectionId)
        {
            this.ConnectionId = connectionId;
        }

        public int CardsNumber;

        public bool WaitForRecconect = false;

        public bool IsReady = false;

        public bool Pass = false;

        public bool Won = false;

        public bool Left = false;

        public int SlotN;

        #region local player only

        public List<string> Cards = new List<string>();

        /// <summary>
        /// Local player only
        /// Take visible cards
        /// </summary>
        public void TakeCards(string[] cards)
        {
            this.Cards.AddRange(cards);
            CardsNumber += cards.Length;
        }

        #endregion

        /// <summary>
        /// Non-local player only
        /// Take invisible cards
        /// </summary>
        public void TakeCards(int amount)
        {
            CardsNumber += amount;
        }
    }
}
