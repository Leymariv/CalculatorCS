using System;
using System.Collections.Generic;
using System.Globalization;
using MyProjects.Calculator.Model;
using MyProjects.Calculator.Presenter;

namespace Calculator.Presenter
{
    public class HandleOperationsTree
    {
        private readonly List<TokenTree> tokensTree;
        public double Result { get; set; }

        private TokenTree head;
        private Dictionary<string, TokenTree> TokenDictionaryTree { get; set; }
        private Dictionary<string, Func<List<double>, double>> OperationDictionary { get; set; }

        public HandleOperationsTree()
        {
            Result = 0;
            TokenDictionaryTree = new Dictionary<string, TokenTree>
                                  {
                                      {
                                          ",", new TokenTree(Result, TypeTokenTree.Number,
                                                             AdvancedTypeTokenTree.Num,
                                                             (int)AdvancedTypeTokenTree.Num, ",")
                                      },
                                      {
                                          "+", new TokenTree(Result, TypeTokenTree.OpBinaire,
                                                             AdvancedTypeTokenTree.OpBinaireLow,
                                                             (int)AdvancedTypeTokenTree.OpBinaireLow, "+")
                                      },
                                      {
                                          "-", new TokenTree(Result, TypeTokenTree.OpBinaire,
                                                             AdvancedTypeTokenTree.OpBinaireLow,
                                                             (int)AdvancedTypeTokenTree.OpBinaireLow, "-")
                                      },
                                      {
                                          "*", new TokenTree(Result, TypeTokenTree.OpBinaire,
                                                             AdvancedTypeTokenTree.OpBinaireHigh,
                                                             (int)AdvancedTypeTokenTree.OpBinaireHigh, "*")
                                      },
                                      {
                                          "/", new TokenTree(Result, TypeTokenTree.OpBinaire,
                                                             AdvancedTypeTokenTree.OpBinaireHigh,
                                                             (int)AdvancedTypeTokenTree.OpBinaireHigh, "/")
                                      },
                                      {
                                          "^", new TokenTree(Result, TypeTokenTree.OpBinaire,
                                                             AdvancedTypeTokenTree.OpBinaireVeryHigh,
                                                             (int)AdvancedTypeTokenTree.OpBinaireVeryHigh, "^")
                                      },
                                      {
                                          "%", new TokenTree(Result, TypeTokenTree.OpUnaire,
                                                             AdvancedTypeTokenTree.OpUnaireRight,
                                                             (int)AdvancedTypeTokenTree.OpUnaireRight, "%")
                                      },
                                      {
                                          "√", new TokenTree(Result, TypeTokenTree.OpUnaire,
                                                             AdvancedTypeTokenTree.OpUnaireLeft,
                                                             (int)AdvancedTypeTokenTree.OpUnaireLeft, "√")
                                      },
                                      {
                                          "¬", new TokenTree(Result, TypeTokenTree.OpUnaire,
                                                             AdvancedTypeTokenTree.OpUnaireLeft,
                                                             (int)AdvancedTypeTokenTree.OpUnaireLeft, "¬")
                                      }
                                  };

            int between0And9 = 10;
            for (int i = 0; i < between0And9; i++)
            {
                TokenDictionaryTree.Add(i.ToString(CultureInfo.CurrentCulture),
                                        new TokenTree(i, TypeTokenTree.Number,
                                                      AdvancedTypeTokenTree.Num,
                                                      (int)AdvancedTypeTokenTree.Num,
                                                      i.ToString(CultureInfo.CurrentCulture)));
            }

            OperationDictionary = new Dictionary<string, Func<List<double>, double>>();
            HandleOperations handleOp = new HandleOperations();
            OperationDictionary = handleOp.OperationDictionary;

            tokensTree = new List<TokenTree>();
        }

        public bool IsOpCorrect(string operation)
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(operation) && CreateListFromString(operation))
            {
                BuildTree();
                ret = IsOperationSyntaxCorrect(head);
            }
            return ret;
        }

        private bool CreateListFromString(string operation)
        {
            tokensTree.Clear();
            for (int i = 0; i < operation.Length; i++)
            {
                string character = operation[i].ToString(CultureInfo.CurrentCulture);
                if (TokenDictionaryTree.ContainsKey(character))
                {
                    TokenTree n = new TokenTree(TokenDictionaryTree[character]);
                    if (tokensTree.Count > 0)
                    {
                        TokenTree previousToken = tokensTree[tokensTree.Count - 1];
                        if (previousToken.TypeToken == TypeTokenTree.Number && n.TypeToken == TypeTokenTree.Number)
                        {
                            previousToken.ExpressionToken += n.ExpressionToken;
                            previousToken.ValToken = double.Parse(previousToken.ExpressionToken,
                                                                  CultureInfo.CurrentCulture);
                        }
                        else
                        {
                            tokensTree.Add(n);
                        }
                    }
                    else
                    {
                        tokensTree.Add(n);
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void BuildTree()
        {
            int indexWorstOp = IndexOfWorstOp(tokensTree);
            List<TokenTree> subTokensLeft = tokensTree.GetRange(0, indexWorstOp);
            List<TokenTree> subTokensRight = tokensTree.GetRange(indexWorstOp + 1, tokensTree.Count - indexWorstOp - 1);
            head = tokensTree[indexWorstOp];
            BuildTreeRecursiv(head, subTokensLeft, subTokensRight);
        }

        private void BuildTreeRecursiv(TokenTree subHead, List<TokenTree> leftPart, List<TokenTree> rightPart)
        {
            if (leftPart != null && leftPart.Count > 0)
            {
                int indexLeft = IndexOfWorstOp(leftPart);
                TokenTree leftTokenTree = leftPart[indexLeft];
                subHead.SonLeft = leftTokenTree;
                if (leftPart.Count > indexLeft)
                {
                    var subTokensRight = leftPart.GetRange(indexLeft + 1, leftPart.Count - indexLeft - 1);
                    var subTokensLeft = leftPart.GetRange(0, indexLeft);
                    BuildTreeRecursiv(subHead.SonLeft, subTokensLeft, subTokensRight);
                }
            }
            if (rightPart != null && rightPart.Count > 0)
            {
                int indexRight = IndexOfWorstOp(rightPart);
                TokenTree rightTokenTree = rightPart[indexRight];
                subHead.SonRight = rightTokenTree;
                if (rightPart.Count > indexRight)
                {
                    var subTokensRight = rightPart.GetRange(indexRight + 1, rightPart.Count - indexRight - 1);
                    var subTokensLeft = rightPart.GetRange(0, indexRight);
                    BuildTreeRecursiv(subHead.SonRight, subTokensLeft, subTokensRight);
                }
            }
        }

        private bool IsOperationSyntaxCorrect(TokenTree currentToken)
        {
            bool ret = true;
            if (currentToken.SonRight == null && currentToken.SonLeft == null)
            {
                if (currentToken.TypeToken != TypeTokenTree.Number)
                {
                    return false;
                }
            }
            else if (currentToken.SonRight == null && currentToken.SonLeft != null)
            {
                if (currentToken.AdvancedTypeToken != AdvancedTypeTokenTree.OpUnaireRight)
                {
                    return false;
                }
                ret = IsOperationSyntaxCorrect(currentToken.SonLeft);
            }
            else if (currentToken.SonRight != null && currentToken.SonLeft == null)
            {
                if (currentToken.AdvancedTypeToken != AdvancedTypeTokenTree.OpUnaireLeft)
                {
                    return false;
                }
                ret = IsOperationSyntaxCorrect(currentToken.SonRight);
            }
            else
            {
                ret = IsOperationSyntaxCorrect(currentToken.SonLeft) && IsOperationSyntaxCorrect(currentToken.SonRight);
            }
            return ret;
        }

        private int IndexOfWorstOp(List<TokenTree> subTokensTree)
        {
            int retIndexWorstOp = 0;
            for (int j = 0; j < subTokensTree.Count; j++)
            {
                TokenTree currentToken = subTokensTree[j];
                if (currentToken.TypeToken != TypeTokenTree.Number)
                {
                    if (currentToken.AdvancedTypeToken == AdvancedTypeTokenTree.OpUnaireRight &&
                        subTokensTree[retIndexWorstOp].AdvancedTypeToken == AdvancedTypeTokenTree.OpUnaireRight)
                    {
                        retIndexWorstOp = j;
                    }
                    if (currentToken.Priority < subTokensTree[retIndexWorstOp].Priority)
                    {
                        retIndexWorstOp = j;
                    }
                }
            }
            return retIndexWorstOp;
        }

        private double ComputeRecursif(TokenTree subHead)
        {
            double retValue = 0;
            TokenTree leftfSonToken = subHead.SonLeft;
            TokenTree rightSonToken = subHead.SonRight;

            string expressionToken = subHead.ExpressionToken;

            if (leftfSonToken == null && rightSonToken == null)
            {
                retValue = subHead.ValToken;
            }

            else if (leftfSonToken == null)
            {
                retValue += ComputeOperationHelper(expressionToken, 0, ComputeRecursif(rightSonToken));
            }

            else if (rightSonToken == null)
            {
                retValue += ComputeOperationHelper(expressionToken, ComputeRecursif(leftfSonToken), 0);
            }

            else
            {
                retValue += ComputeOperationHelper(expressionToken,
                                                   ComputeRecursif(leftfSonToken),
                                                   ComputeRecursif(rightSonToken));
            }
            return retValue;
        }

        private double ComputeOperationHelper(string expressionToken,
                                              double leftfSon, double rightSon)
        {
            double toConcat = 0;
            Func<List<double>, double> func;
            OperationDictionary.TryGetValue(expressionToken, out func);
            if (func != null)
            {
                List<double> parameters = new List<double>
                                          {
                                              leftfSon,
                                              rightSon
                                          };
                toConcat = func(parameters);
            }
            return toConcat;
        }

        public bool ComputeOperation(string operation)
        {
            bool retValue = IsOpCorrect(operation);
            Result = 0;
            if (retValue)
            {
                Result = ComputeRecursif(head);
            }
            return retValue;
        }
    }
}