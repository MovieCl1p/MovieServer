using System;

namespace Game.Services
{
    public interface IQuizService
    {
        QuizData GetQuiz();

        QuizData GetMockQuiz();

        void Init();
    }
}
