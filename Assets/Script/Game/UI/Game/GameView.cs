using System;
using Core.Binder;
using Core.ViewManager;
using Game.Data;
using Game.Services;
using Game.Services.Instance;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Game
{
    public class GameView : BaseView
    {
        [SerializeField]
        private QuestionsView _questionView;

        [SerializeField]
        private Text _question;

        private IInstanceService _instanceService;

        protected override void Start()
        {
            base.Start();

            _instanceService = BindManager.GetInstance<IInstanceService>();
            _instanceService.StartGame();

            _instanceService.OnFinishRound += OnFinishRaund;
            _instanceService.OnStartRound += OnStartRaund;
            _instanceService.OnFinishGame += OnFinishGame;

            ShowNextQuiz();
        }

        private void OnFinishGame()
        {
            ViewManager.Instance.SetView(ViewNames.MainMenu);
        }

        private void OnStartRaund()
        {
            ShowNextQuiz();
        }

        private void OnFinishRaund()
        {
            _questionView.HighlightCorrectAnswer();
            _questionView.RefreshView(null);
        }

        private void ShowNextQuiz()
        {
            _questionView.OnPlayerAnswer += OnPlayerAnwer;

            QuizData data = _instanceService.GetNextQuiz();

            _questionView.RefreshView(data);
            _question.text = data.Question;
        }

        private void OnPlayerAnwer(AnswerView answerView)
        {
            IInstanceService network = BindManager.GetInstance<IInstanceService>();
            network.PlayerAnswer(answerView.Data);

            _questionView.OnPlayerAnswer -= OnPlayerAnwer;
        }

        protected override void OnReleaseResources()
        {
            _questionView.OnPlayerAnswer -= OnPlayerAnwer;
            _instanceService.OnFinishRound -= OnFinishRaund;
            _instanceService.OnStartRound -= OnStartRaund;
            _instanceService.OnFinishGame -= OnFinishGame;
            base.OnReleaseResources();
        }
    }
}
