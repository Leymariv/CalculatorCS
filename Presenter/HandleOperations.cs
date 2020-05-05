using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MyProjects.Calculator;
using MyProjects.Calculator.Model;

namespace Calculator.Presenter
{
    public class HandleOperations
    {
        private readonly List<Token> tokens;
        public double Result { get; set; }

        private Dictionary<string, Token> TokenDictionary { get; set; }
        public Dictionary<string, Func<List<double>, double>> OperationDictionary { get; set; }

        public HandleOperations()
        {
            Result = 0;
            TokenDictionary = new Dictionary<string, Token>
                              {
                                  {
                                      ",", new Token(Result, TypeToken.Number,
                                                     AdvancedTypeToken.Num,
                                                     (int)AdvancedTypeToken.Num, ",")
                                  },
                                  {
                                      "+", new Token(Result, TypeToken.OpBinaire,
                                                     AdvancedTypeToken.OpBinaireLow,
                                                     (int)AdvancedTypeToken.OpBinaireLow, "+")
                                  },
                                  {
                                      "-", new Token(Result, TypeToken.OpBinaire,
                                                     AdvancedTypeToken.OpBinaireLow,
                                                     (int)AdvancedTypeToken.OpBinaireLow, "-")
                                  },
                                  {
                                      "*", new Token(Result, TypeToken.OpBinaire,
                                                     AdvancedTypeToken.OpBinaireHigh,
                                                     (int)AdvancedTypeToken.OpBinaireHigh, "*")
                                  },
                                  {
                                      "/", new Token(Result, TypeToken.OpBinaire,
                                                     AdvancedTypeToken.OpBinaireHigh,
                                                     (int)AdvancedTypeToken.OpBinaireHigh, "/")
                                  },
                                  {
                                      "^", new Token(Result, TypeToken.OpBinaire,
                                                     AdvancedTypeToken.OpBinaireVeryHigh,
                                                     (int)AdvancedTypeToken.OpBinaireVeryHigh, "^")
                                  },
                                  {
                                      "%", new Token(Result, TypeToken.OpUnaire,
                                                     AdvancedTypeToken.OpUnaireRight,
                                                     (int)AdvancedTypeToken.OpUnaireRight, "%")
                                  },
                                  {
                                      "√", new Token(Result, TypeToken.OpUnaire,
                                                     AdvancedTypeToken.OpUnaireLeft,
                                                     (int)AdvancedTypeToken.OpUnaireRight, "√")
                                  },
                                  {
                                      "¬", new Token(Result, TypeToken.OpUnaire,
                                                     AdvancedTypeToken.OpUnaireLeft,
                                                     (int)AdvancedTypeToken.OpUnaireRight, "¬")
                                  }
                              };

            int between0And9 = 10;
            for (int i = 0; i < between0And9; i++)
            {
                Token toAdd = new Token(i, TypeToken.Number,
                                        AdvancedTypeToken.Num,
                                        (int)AdvancedTypeToken.Num, i.ToString(CultureInfo.CurrentCulture));
                TokenDictionary.Add(i.ToString(CultureInfo.CurrentCulture), toAdd);
            }

            OperationDictionary = new Dictionary<string, Func<List<double>, double>>();
            OperationDictionary["+"] = a => a[0] + a[1];
            OperationDictionary["-"] = a => a[0] - a[1];
            OperationDictionary["*"] = a => a[0] * a[1];
            OperationDictionary["/"] = a => a[0] / a[1];
            OperationDictionary["^"] = a => Math.Pow(a[0], a[1]);
            int pourcent = 100;
            OperationDictionary["%"] = a => a[0] / pourcent;
            OperationDictionary["√"] = a => Math.Sqrt(a[1]);
            OperationDictionary["¬"] = a => -Math.Abs(a[1]);

            tokens = new List<Token>();
        }

        private string FormatOperation(string operation)
        {
            string nameCulture = CultureInfo.CurrentCulture.Name;
            string newOperation = operation;
            if (nameCulture == "en-US")
            {
                newOperation = operation.Replace('.', ',');
            }
            return newOperation;
        }

        public bool IsOpCorrect(string operation)
        {
            string formatedOperation = FormatOperation(operation);
            bool ret = false;
            if (!string.IsNullOrEmpty(formatedOperation) && CreateListFromString(formatedOperation))
            {
                ret = IsOperationSyntaxCorrect();
            }
            return ret;
        }

        private bool NextNotCorrect(Token currToken, Token nextToken)
        {
            bool isSuivCorrect;
            if (currToken.TypeToken == TypeToken.OpBinaire)
            {
                isSuivCorrect = nextToken.TypeToken == TypeToken.Number ||
                                nextToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireLeft;
            }

            else if (currToken.TypeToken == TypeToken.Number)
            {
                isSuivCorrect = nextToken.TypeToken == TypeToken.Number ||
                                nextToken.TypeToken == TypeToken.OpBinaire ||
                                nextToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireRight;
            }
            else
            {
                if (currToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireLeft)
                {
                    isSuivCorrect = nextToken.TypeToken == TypeToken.Number ||
                                    nextToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireLeft;
                }
                else
                {
                    isSuivCorrect = nextToken.TypeToken == TypeToken.OpBinaire ||
                                    nextToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireRight;
                }
            }
            return !isSuivCorrect;
        }

        private bool FirstOrLastNotCorrect(Token firstToken, Token lastToken)
        {
            bool ret = true;
            if (firstToken != null && lastToken != null)
            {
                ret = firstToken.TypeToken == TypeToken.OpBinaire ||
                      firstToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireRight ||
                      lastToken.TypeToken == TypeToken.OpBinaire ||
                      lastToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireLeft;
            }
            return ret;
        }

        private bool IsOperationSyntaxCorrect()
        {
            int stopIndex = 1;
            for (int i = 0; i < tokens.Count - stopIndex; i++)
            {
                Token currToken = tokens[i];
                Token suivToken = tokens[i + stopIndex];

                if (NextNotCorrect(currToken, suivToken))
                {
                    return false;
                }
            }
            Token first = null;
            Token last = null;
            if (tokens != null && tokens.Count > 1)
            {
                first = tokens.First();
                last = tokens.Last();
            }
            return !FirstOrLastNotCorrect(first, last);
        }

        private bool CreateListFromString(string operation)
        {
            tokens.Clear();
            for (int i = 0; i < operation.Length; i++)
            {
                string character = operation[i].ToString(CultureInfo.CurrentCulture);
                if (TokenDictionary.ContainsKey(character))
                {
                    Token n = new Token(TokenDictionary[character]);
                    if (tokens.Count > 0)
                    {
                        Token previousToken = tokens[tokens.Count - 1];
                        if (previousToken.TypeToken == TypeToken.Number && n.TypeToken == TypeToken.Number)
                        {
                            previousToken.ExpressionToken += n.ExpressionToken;
                            previousToken.ValToken = double.Parse(previousToken.ExpressionToken,
                                                                  CultureInfo.CurrentCulture);
                        }
                        else
                        {
                            tokens.Add(n);
                        }
                    }
                    else
                    {
                        tokens.Add(n);
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private int IndexOfBestOp()
        {
            int retIndexBestOp = 0;
            for (int j = 0; j < tokens.Count; j++)
            {
                Token currentToken = tokens[j];
                Token highestPriorityToken = tokens[retIndexBestOp];

                if (currentToken.Priority == highestPriorityToken.Priority &&
                    currentToken.AdvancedTypeToken == highestPriorityToken.AdvancedTypeToken &&
                    currentToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireLeft)
                {
                    retIndexBestOp = j;
                }

                if (tokens[j].Priority > tokens[retIndexBestOp].Priority)
                {
                    retIndexBestOp = j;
                }
            }
            return retIndexBestOp;
        }

        private void ConcatElementAfterOperation(int indexBestOp)
        {
            Token highestPriorityToken = tokens[indexBestOp];
            int indexMinus = indexBestOp - 1;
            int indexPlus = indexBestOp + 1;

            if (highestPriorityToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireLeft)
            {
                tokens.RemoveAt(indexPlus);
            }
            else if (highestPriorityToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireRight)
            {
                tokens.RemoveAt(indexMinus);
            }
            else
            {
                tokens.RemoveAt(indexMinus);
                tokens.RemoveAt(indexBestOp);
            }
        }

        public bool ComputeOperation(string operation)
        {
            bool retValue = true;
            Result = 0;
            if (IsOpCorrect(operation))
            {
                while (tokens.Count > 1)
                {
                    int indexBestOp = IndexOfBestOp();
                    /* At this point we are sure that we have the highest Priority operation */
                    Token highestPriorityToken = tokens[indexBestOp];
                    var resultatOpAtomic = AtomicOperationResult(indexBestOp);
                    ConcatElementAfterOperation(indexBestOp);
                    highestPriorityToken.ExpressionToken = string.Empty;
                    highestPriorityToken.AdvancedTypeToken = AdvancedTypeToken.Num;
                    highestPriorityToken.Priority = 0;
                    highestPriorityToken.ValToken = resultatOpAtomic;
                }
                Result = tokens[0].ValToken;
            }
            else
            {
                retValue = false;
            }
            return retValue;
        }

        public double AtomicOperationResult(int currentElement)
        {
            double val1 = 0;
            double val2 = 0;
            Token currentToken = tokens[currentElement];
            string op = currentToken.ExpressionToken;
            int overfolw = 1;

            if (tokens[currentElement].TypeToken == TypeToken.OpBinaire)
            {
                val1 = tokens[currentElement - overfolw].ValToken;
                val2 = tokens[currentElement + overfolw].ValToken;
            }
            else
            {
                if (currentToken.AdvancedTypeToken == AdvancedTypeToken.OpUnaireLeft)
                {
                    val2 = tokens[currentElement + overfolw].ValToken;
                }
                else
                {
                    val1 = tokens[currentElement - overfolw].ValToken;
                }
            }
            List<double> parameters = new List<double>
                                      {
                                          val1,
                                          val2
                                      };
            return OperationDictionary[op](parameters);
        }
    }
}