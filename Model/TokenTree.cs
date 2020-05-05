namespace MyProjects.Calculator.Model
{
    public class TokenTree
    {
        public TypeTokenTree TypeToken { get; set; }
        public AdvancedTypeTokenTree AdvancedTypeToken { get; set; }
        public int Priority { get; set; }
        public double ValToken { get; set; }
        public string ExpressionToken { get; set; }
        public TokenTree SonRight { get; set; }
        public TokenTree SonLeft { get; set; }

        public TokenTree(double valueToken, TypeTokenTree typeToken, AdvancedTypeTokenTree advancedTypeToken,
                         int priority, string expressionToken)
        {
            ValToken = valueToken;
            TypeToken = typeToken;
            AdvancedTypeToken = advancedTypeToken;
            Priority = priority;
            ExpressionToken = expressionToken;
            SonLeft = null;
            SonRight = null;
        }

        public TokenTree(TokenTree token)
        {
            ValToken = token.ValToken;
            TypeToken = token.TypeToken;
            AdvancedTypeToken = token.AdvancedTypeToken;
            Priority = token.Priority;
            ExpressionToken = token.ExpressionToken;
            SonLeft = null;
            SonRight = null;
        }
    }
}