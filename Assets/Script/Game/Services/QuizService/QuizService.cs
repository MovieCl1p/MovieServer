using System;
using System.Collections.Generic;

namespace Game.Services
{
    public class QuizService : IQuizService
    {
        private List<QuizData> _quizes;

        public void Init()
        {
            _mockIndex = 0;
            _quizes = new List<QuizData>();
            MockQuiz();
        }

        public QuizData GetQuiz()
        {
            return _quizes[0];
        }

        private void MockQuiz()
        {
            string id = Guid.NewGuid().ToString();
            string question = "What are most popular movie 2017?";
            string[] answers = new string[] { "Tor", "Avangers", "Mission imposible", "Day1"};
            int answer = 0;
            QuizData data = new QuizData(id, question, answers, answer);
            _quizes.Add(data);

            id = Guid.NewGuid().ToString();
            question = "Movie 2018?";
            answers = new string[] { "Deadpool 2", "Venom", "First Man", "Black Panther" };
            answer = 1;
            data = new QuizData(id, question, answers, answer);
            _quizes.Add(data);

            id = Guid.NewGuid().ToString();
            question = "Question 3?";
            answers = new string[] { "1", "2", "3", "42" };
            answer = 3;
            data = new QuizData(id, question, answers, answer);

            _quizes.Add(data);
        }

        private int _mockIndex = 0;
        public QuizData GetMockQuiz()
        {
            if(++_mockIndex >= _quizes.Count - 1)
            {
                return null;
            }

            return _quizes[_mockIndex];
        }
    }
}
