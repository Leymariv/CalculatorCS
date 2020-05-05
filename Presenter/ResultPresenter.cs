using System;
using System.Globalization;
using Calculator.Presenter;
using MyProjects.Calculator.View;

namespace MyProjects.Calculator.Presenter
{
    public class ResultPresenter
    {
        public event Action OnModification = delegate { };
        public InputView InputView { get; set; }
        public ShowResultView ResultView { get; set; }
        private readonly HandleOperations computeAndHandleOperations;

        public string Operation { get; set; }
        public string CurrentResult { get; set; }
        public int Cursor { get; set; }

        private ResultPresenter()
        {
            Cursor = 0;
            Operation = string.Empty;
            CurrentResult = string.Empty;
            computeAndHandleOperations = new HandleOperations();
        }

        private static readonly ResultPresenter instance = new ResultPresenter();

        public static ResultPresenter Instance
        {
            get { return instance; }
        }

        public void RefreshResult(string res)
        {
            CurrentResult = res;
            OnModification();
        }

        public void RefreshOperation(string op)
        {
            Operation = op;
            OnModification();
        }

        public void CalculateOperation()
        {
            if (computeAndHandleOperations.ComputeOperation(Operation))
            {
                CurrentResult = computeAndHandleOperations.Result.ToString(CultureInfo.CurrentCulture);
            }
            OnModification();
        }

        public void RefreshCursor(int position)
        {
            Cursor = position;
            OnModification();
        }
    }
}