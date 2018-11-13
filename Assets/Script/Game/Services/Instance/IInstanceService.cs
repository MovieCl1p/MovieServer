
using System;

namespace Game.Services.Instance
{
    public interface IInstanceService
    {
        event Action OnFinishRound;
        event Action OnStartRound;
        event Action OnFinishGame;

        void PlayerAnswer(string data); 

        void StartGame();

        QuizData GetNextQuiz();
    }
}
