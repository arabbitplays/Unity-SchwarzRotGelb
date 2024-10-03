using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    private List<Card> cards, usedCards;

    private Card currCard;
    private Card lastCard;

    private Vector2 lastCardPos = new Vector3(-1, 0, 0);
    private Vector2 currCardPos = new Vector3(1, 0, 0);

    [SerializeField] private float drawAnimationTime, pickAnimationTime;
    [SerializeField] private Transform stackTransform;
    private bool animating;

    private ScoreManager scoreManager;

    private void Awake() {
        scoreManager = GetComponent<ScoreManager>();
    }

    void Start()
    {
        GenerateCards();
        currCard = GetRandomCard();
        SetAsLastCard(GetRandomCard());
        currCard.transform.position = currCardPos;
        lastCard.transform.position = lastCardPos;
    }

    private void GenerateCards() {
        cards = new List<Card>();
        usedCards = new List<Card>();

        for (int wordCount = 1; wordCount <= 5; wordCount++) {
            for (int colorWord = 0; colorWord < Card.colorWords.Length; colorWord++) {
                for (int color = 0; color < Card.colors.Length; color++) {
                    if (colorWord == color)
                        continue;
                    GameObject obj = Instantiate(cardPrefab);
                    Card card = obj.GetComponent<Card>();
                    card.Init(wordCount, colorWord, color);
                    obj.SetActive(false);
                    cards.Add(card);
                }
            }
        }
    }

    public void PickCard() {
        if (animating)
            return;

        if (Card.IsValidCombination(lastCard, currCard)) {
            scoreManager.IncreaseScore();
        } else {
            scoreManager.ResetScore();
        }

        StartCoroutine(PickRoutine());
    }

    public void RejectCard() {
        if (animating)
            return;

        if (!Card.IsValidCombination(lastCard, currCard)) {
            scoreManager.IncreaseScore();
        } else {
            scoreManager.ResetScore();
        }

        StartCoroutine(RejectRoutine());
    }

    private IEnumerator RejectRoutine() {
        animating = true;
        yield return CardMoveRoutine(currCard, new Vector3(3, 0, 0));
        currCard.gameObject.SetActive(false);
        yield return StartCoroutine(DrawRoutine());
        animating = false;
    }

    private IEnumerator PickRoutine() {
        animating = true;
        yield return StartCoroutine(CardMoveRoutine(currCard, lastCardPos));

        lastCard.gameObject.SetActive(false);
        SetAsLastCard(currCard);

        yield return StartCoroutine(DrawRoutine());
        animating = false;
    }

    private IEnumerator CardMoveRoutine(Card card, Vector3 endPos) {
        yield return StartCoroutine(AnimationUtil.AnimateTranslation(currCard.transform, endPos, pickAnimationTime, EasingFunctions.EaseInEaseOutSin));
    }

    private IEnumerator DrawRoutine() {
        yield return StartCoroutine(AnimationUtil.AnimateRotation(stackTransform, Quaternion.Euler(0, 90, 0), drawAnimationTime / 2, EasingFunctions.LinearEasing));
        stackTransform.rotation = Quaternion.Euler(0, 0, 0);

        currCard = GetRandomCard();
        currCard.transform.position = currCardPos;

        currCard.transform.rotation = Quaternion.Euler(0, 90, 0);
        StartCoroutine(AnimationUtil.AnimateRotation(currCard.transform, Quaternion.Euler(0, 0, 0), drawAnimationTime / 2, EasingFunctions.LinearEasing));
    }

    private float EaseInEaseOutSin(float t) {
        return (-Mathf.Cos(Mathf.PI * t) + 1) / 2;
    }

    private void SetAsLastCard(Card card) {
        lastCard = card;
        lastCard.GetComponentInChildren<SpriteRenderer>().sortingOrder = -2;
        lastCard.GetComponentInChildren<Canvas>().sortingOrder = -1;
    }

    private Card GetRandomCard() {
        if (cards.Count == 0) {
            cards = usedCards;
            usedCards = new List<Card>();
        }

        int rand = Random.Range(0, cards.Count);
        Card card = cards[rand];
        card.gameObject.SetActive(true);
        cards.Remove(card);
        usedCards.Add(card);
        return card;
    }
}
