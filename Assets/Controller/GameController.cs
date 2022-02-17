using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private GameFacade _gameFacade;
    private Text _titleText;
    private GameObject _btnMenuPrefab;
    private GameObject _btnCardPrefab;
    private GameObject _parentPanel;

    [SerializeField]
    private CardBundleData _cardBundleData;


    private GameStateAbstract _gameState;
    private GameObject _btnMenuGO;
    private Button _btnMenu;
    private List<Button> _btnCards;
    private List<string> _listOfCorrectAnswers = new List<string>();


    void Start()
    {
        _titleText = GameObject.Find("Title").GetComponent<Text>();
        _btnMenuPrefab = (GameObject)Resources.Load("prefabs/BtnMenuPrefab", typeof(GameObject));
        _btnCardPrefab = (GameObject)Resources.Load("prefabs/BtnCardPrefab", typeof(GameObject));
        _parentPanel = GameObject.Find("Canvas");

        _gameFacade = (GameFacade)ScriptableObject.CreateInstance("GameFacade");

        _btnMenuGO = Instantiate(_btnMenuPrefab, _parentPanel.transform);

        this.stateTransitionTo(new GameMenuState());
        _gameState.init(ref _titleText, ref _btnMenu, ref _btnMenuGO, ref _parentPanel, ref _btnCardPrefab, null, startGame, ref _listOfCorrectAnswers, ref _btnCards);


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Camera.main.DOShakeRotation(1, 5, 10, 50, true);
            Camera.main.DOShakePosition(3);
        }
    }



    private void startGame()
    {
        //Debug.Log("GameController startGame");
        _listOfCorrectAnswers = new List<string>();
        this.stateTransitionTo(new GamePlayingState());
        _btnMenuGO.SetActive(false);
        _btnCards =_gameState.init(ref _titleText, ref _btnMenu, ref _btnMenuGO, ref _parentPanel, ref _btnCardPrefab, _cardBundleData, successAnswerOnLevel, ref _listOfCorrectAnswers, ref _btnCards);
    }

    private void reStartGame()
    {
        //Debug.Log("GameController reStartGame");
        startGame();
    }

    private void stateTransitionTo(GameStateAbstract gameStateAbstract)
    {
        _gameState = gameStateAbstract;
    }

    private void successAnswerOnLevel()
    {
        //Debug.Log("successAnswerOnLevel");
        _gameFacade.destroyButtons(ref _btnCards);

        if (_btnCards != null)
        {
            if (_btnCards.Count > 8)
            {
                _gameFacade.destroyButton(ref _btnMenu);
                _btnCards = new List<Button>();
                _btnMenuGO = Instantiate(_btnMenuPrefab, _parentPanel.transform);
                _btnMenuGO.SetActive(true);
                this.stateTransitionTo(new GameEndState());
                _btnCards =_gameState.init(ref _titleText, ref _btnMenu, ref _btnMenuGO, ref _parentPanel, ref _btnCardPrefab, _cardBundleData, reStartGame, ref _listOfCorrectAnswers, ref _btnCards);
                return;
            }
        }

        this.stateTransitionTo(new GamePlayingState());
        _btnCards =_gameState.init(ref _titleText, ref _btnMenu, ref _btnMenuGO, ref _parentPanel, ref _btnCardPrefab, _cardBundleData, successAnswerOnLevel, ref _listOfCorrectAnswers, ref _btnCards);
    }


}
