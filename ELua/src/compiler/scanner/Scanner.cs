using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Scanner {

		private static readonly HashSet<char> _Operator = new HashSet<char>(new[] { '(', ')', '[', ']', '{', '}', '=', '!', '>', '<', ',', '+', '-', '*', '/', '.', ':', '&', '^', '|', '?', '%', '~', '#', ';' });
		private static readonly Regex _Keyword = new Regex(@"\b(and|break|do|else|elseif|end|false|for|function|if|in|local|nil|not|or|repeat|return|then|true|until|while)\b", RegexOptions.Compiled);
		private static readonly Regex _Line = new Regex(@"\r\n|\n", RegexOptions.Compiled);
		private static readonly Regex _Ignored = new Regex(@"\-\-\[\[(\n|.)*?\]\]|\-\-.*?(?=(\r\n|\n|$))|(\t| )+", RegexOptions.Compiled);
		private static readonly Regex _CData = new Regex(@"\[\[(\n|.)*?\]\]", RegexOptions.Compiled);
		private static readonly Regex _String = new Regex(@"""(\\""|.)*?""|'(\\""|.)*?'", RegexOptions.Compiled);
		private static readonly Regex _Word = new Regex(@"\b[_a-zA-Z]\w*\b", RegexOptions.Compiled);
		private static readonly Regex _Number = new Regex(@"\b\d+\.\d+|\d+\b", RegexOptions.Compiled);

		private readonly Dictionary<Regex, Match> _matches = new Dictionary<Regex, Match>();
		private readonly List<Token> _tokens = new List<Token>();
		private readonly string _file;
		private readonly string _src;
		private int _line = 1;
		private int _position;

		public IList<Token> Tokens {
			get { return _tokens; }
		}

		public Scanner(string file, string src) {
			_file = file;
			_src = src;
			Parse();
		}

		private bool IsMatch(Regex regex, out Match match) {
			if (!_matches.TryGetValue(regex, out match)) {
				return TryMatch(regex, out match);
			} else {
				return NextMatch(regex, ref match);
			}
		}

		private bool TryMatch(Regex regex, out Match match) {
			match = regex.Match(_src, _position);
			if (!match.Success | match.Index > _position) {
				_matches[regex] = match;
				return false;
			} else if (match.Index == _position) {
				return true;
			} else {
				throw new Exception(string.Format("TryMatch error, file={0}, line={1}, match={2}, last={3}", _file, _line, match, _tokens.Last()));
			}
		}

		private bool NextMatch(Regex regex, ref Match match) {
			if (!match.Success | match.Index > _position) {
				return false;
			} else if (match.Index == _position) {
				return true;
			} else {
				return TryMatch(regex, out match);
			}
		}

		private void Parse() {
			while (_position < _src.Length) {
				Match match;
				if (IsMatch(_Line, out match)) {
					_line += 1;
					_position += match.Length;
				} else if (IsMatch(_Ignored, out match)) {
					_line += _Line.Matches(match.Value).Count;
					_position += match.Length;
				} else if (IsMatch(_CData, out match)) {
					_tokens.Add(new Token { type = Token.Type.String, value = _src.Substring(_position + 2, match.Length - 4), index = _tokens.Count, line = _line, position = _position, file = _file });
					_line += _Line.Matches(match.Value).Count;
					_position += match.Length;
				} else if (IsMatch(_String, out match)) {
					_tokens.Add(new Token { type = Token.Type.String, value = _src.Substring(_position + 1, match.Length - 2), index = _tokens.Count, line = _line, position = _position, file = _file });
					_position += match.Length;
				} else if (_Operator.Contains(_src[_position])) {
					_tokens.Add(new Token { type = Token.Type.Operator, value = _src[_position].ToString(), index = _tokens.Count, line = _line, position = _position, file = _file });
					_position += 1;
				} else if (IsMatch(_Keyword, out match)) {
					_tokens.Add(new Token { type = Token.Type.Keyword, value = _src.Substring(_position, match.Length), index = _tokens.Count, line = _line, position = _position, file = _file });
					_position += match.Length;
				} else if (IsMatch(_Word, out match)) {
					_tokens.Add(new Token { type = Token.Type.Word, value = _src.Substring(_position, match.Length), index = _tokens.Count, line = _line, position = _position, file = _file });
					_position += match.Length;
				} else if (IsMatch(_Number, out match)) {
					_tokens.Add(new Token { type = Token.Type.Number, value = _src.Substring(_position, match.Length), index = _tokens.Count, line = _line, position = _position, file = _file });
					_position += match.Length;
				} else {
					throw new Exception(string.Format("Parse error, file={0}, line={1}, char={2}, last={3}", _file, _line, _src[_position], _tokens.Last()));
				}
			}
		}

	}

}

