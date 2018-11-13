using System;

namespace Game.Services
{
    public class QuizData
    {
        private readonly string _id;
        private readonly string _question;
        private readonly string[] _answers;
        private readonly int _answer;

        public QuizData()
        { 
        }

        public QuizData(string id, string question, string[] answers, int answer)
        {
            _id = id;
            _question = question;
            _answers = answers;
            _answer = answer;
        }

        public string Question
        {
            get
            {
                return _question;
            }
        }

        public string[] Answers
        {
            get
            {
                return _answers;
            }
        }

        public int Answer
        {
            get
            {
                return _answer;
            }
        }
    }
}
