using System;
using System.Windows.Forms;
using MyProjects.Calculator.Presenter;

namespace MyProjects.Calculator.View
{
    public partial class InputView : UserControl
    {
        private readonly ResultPresenter presenter;

        public InputView()
        {
            InitializeComponent();
            presenter = ResultPresenter.Instance;
            presenter.InputView = this;
        }

        private void ButtonBackspaceClick(object sender, EventArgs e)
        {
            if (presenter.Operation.Length > 0)
            {
                presenter.Operation = presenter.Operation.TrimEnd(presenter.Operation[presenter.Operation.Length - 1]);
                presenter.RefreshOperation(presenter.Operation);
            }
        }

        private void ButtonSqrtClick(object sender, EventArgs e)
        {
            AnyButtonClick(ButtonSqrt);
        }

        private void ButtonCClick(object sender, EventArgs e)
        {
            presenter.Operation = string.Empty;
            presenter.RefreshOperation(presenter.Operation);
            presenter.Cursor = 0;
            presenter.RefreshResult("0");
        }

        private void ButtonCeClick(object sender, EventArgs e)
        {
            presenter.Cursor = 0;
            presenter.RefreshResult("0");
        }

        private void ButtonNegateClick(object sender, EventArgs e)
        {
            AnyButtonClick(ButtonNegate);
        }

        private void ButtonPlusClick(object sender, EventArgs e)
        {
            AnyButtonClick(ButtonPlus);
        }

        private void Button0Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button0);
        }

        private void ButtonComaClick(object sender, EventArgs e)
        {
            AnyButtonClick(ButtonComa);
        }

        private void ButtonMinusClick(object sender, EventArgs e)
        {
            AnyButtonClick(ButtonMinus);
        }

        private void ButtonEqualClick(object sender, EventArgs eventArgs)
        {
            presenter.CalculateOperation();
        }

        private void ButtonTimesClick(object sender, EventArgs e)
        {
            AnyButtonClick(ButtonTimes);
        }

        private void ButtonDivideClick(object sender, EventArgs e)
        {
            AnyButtonClick(ButtonDivide);
        }

        private void ButtonPowClick(object sender, EventArgs e)
        {
            AnyButtonClick(ButtonPow);
        }

        private void Button6Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button6);
        }

        private void Button7Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button7);
        }

        private void Button8Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button8);
        }

        private void Button9Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button9);
        }

        private void ButtonPourcentClick(object sender, EventArgs e)
        {
            AnyButtonClick(ButtonPourcent);
        }

        private void Button5Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button5);
        }

        private void Button4Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button4);
        }

        private void Button3Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button3);
        }

        private void Button2Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button2);
        }

        private void Button1Click(object sender, EventArgs e)
        {
            AnyButtonClick(Button1);
        }

        private void AnyButtonClick(Button button)
        {
            int cursor = presenter.Cursor;
            presenter.Operation = presenter.Operation.Insert(cursor, button.Text);
           /* maj de TextBoxOp dans ShowResultView */
            presenter.RefreshOperation(presenter.Operation);
        }
    }
}