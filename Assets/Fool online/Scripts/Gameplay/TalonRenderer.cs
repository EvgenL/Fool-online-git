﻿using DG.Tweening;
using Fool_online.Scripts.FoolNetworkScripts.NetworksObserver;
using Fool_online.Scripts.InRoom.CardsScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Fool_online.Scripts.InRoom
{

    /// <summary>
    /// Class displayng talon (прикуп)
    /// </summary>
    public class TalonRenderer : MonoBehaviourFoolObserver
    {
        [Header("Talon")]
        public Text CardsLeftText;
        public Image TalonDisplay;

        [Header("Trump card")]
        public Image TrumpCard;
        public Image TrumpSuitIcon;

        [Header("")]
        public Sprite SpadesSprite;
        public Sprite HeartsSprite;
        public Sprite DiamondsSprite;
        public Sprite ClubsSprite;

        private Sprite[] _suits;

        public Vector3 hidPosition;
        private Vector3 showPosition;

        private int cardsInTalon;

        private void Awake()
        {
            showPosition = transform.position;

            transform.position = hidPosition;

            _suits = new Sprite[] {SpadesSprite, HeartsSprite, DiamondsSprite, ClubsSprite};
        }


        public void HideTalon(float delay)
        {
            transform.DOMove(hidPosition, 1f).SetDelay(delay);
        }

        private void ShowTalon(int cards, string talonCardCode = null)
        {
            transform.DOMove(showPosition, 1f);
            cardsInTalon = cards;
            CardsLeftText.text = cards.ToString();
            TalonDisplay.enabled = true;
            TrumpCard.enabled = true;
            TrumpSuitIcon.enabled = false;

            if (cards <= 1)
            {
                TalonDisplay.enabled = false;
            }

            if (cards == 0)
            {
                TrumpCard.enabled = false;
                TrumpSuitIcon.enabled = true;
            }

            if (talonCardCode != null)
            {
                TrumpCard.sprite = CardUtil.GetSprite(talonCardCode);

                int suitNumber = CardUtil.Suit(talonCardCode);
                TrumpSuitIcon.sprite = _suits[suitNumber];
            }
        }

        //observed callback
        public override void OnTalonData(int talonSize, string trumpCardCode)
        {
            ShowTalon(talonSize, trumpCardCode);
        }

        public override void OnYouGotCards(string[] cards)
        {
            cardsInTalon -= cards.Length;
            ShowTalon(cardsInTalon);
        }

        public override void OnEnemyGotCardsFromTalon(long playerId, int slotN, int cardsN)
        {
            cardsInTalon -= cardsN;
            ShowTalon(cardsInTalon);
        }
    }
}
