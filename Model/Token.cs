namespace MyProjects.Calculator.Model
{
    public class Token
    {
        public TypeToken TypeToken { get; set; }
        public AdvancedTypeToken AdvancedTypeToken { get; set; }
        public int Priority { get; set; }
        public double ValToken { get; set; }

        public string ExpressionToken { get; set; }

        public Token(double valueToken, TypeToken typeToken, AdvancedTypeToken advancedTypeToken,
                     int priority, string expressionToken)
        {
            ValToken = valueToken;
            TypeToken = typeToken;
            AdvancedTypeToken = advancedTypeToken;
            Priority = priority;
            ExpressionToken = expressionToken;
        }

        public Token(Token token)
        {
            ValToken = token.ValToken;
            TypeToken = token.TypeToken;
            AdvancedTypeToken = token.AdvancedTypeToken;
            Priority = token.Priority;
            ExpressionToken = token.ExpressionToken;
        }
    }
}