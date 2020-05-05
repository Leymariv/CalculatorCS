using System.Globalization;
using System.Windows.Forms;
using MyProjects.Calculator.Presenter;

namespace MyProjects.Calculator.View
{
    public partial class ShowResultView : UserControl
    {
        private readonly ResultPresenter presenter;

        public ShowResultView()
        {
            InitializeComponent();
            presenter = ResultPresenter.Instance;
            presenter.ResultView = this;
            presenter.OnModification += RefreshOperation;
            presenter.OnModification += RefreshResult;
        }

        private void RefreshOperation()
        {
            textBoxOp.SelectionStart = presenter.Cursor;
            textBoxOp.Focus();
            textBoxOp.Text = ResultPresenter.Instance.Operation;
        }

        private void RefreshResult()
        {
            textBoxResult.Text = ResultPresenter.Instance.CurrentResult;
        }

        private void TextBoxOpKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                presenter.CalculateOperation();
            }
        }

        private void TextBoxOpTextChanged(object sender, System.EventArgs e)
        {
            presenter.Operation += e.ToString();
            presenter.Cursor++;
            presenter.RefreshOperation(textBoxOp.Text);
        }

        private void TextBoxOpClick(object sender, System.EventArgs e)
        {
            presenter.RefreshCursor(textBoxOp.SelectionStart);
        }
    }
}