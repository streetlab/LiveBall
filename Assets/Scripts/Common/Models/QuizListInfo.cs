using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuizListInfo {
	GameTypeInfo _gameType;

	public GameTypeInfo gameType {
		get {
			return _gameType;
		}
		set {
			_gameType = value;
		}
	}
	List<TeamInfo> _team;

	public List<TeamInfo> team {
		get {
			return _team;
		}
		set {
			_team = value;
		}
	}

	List<PlayerInfo> _player;

	public List<PlayerInfo> player {
		get {
			return _player;
		}
		set {
			_player = value;
		}
	}

	List<QuizInfo> _quiz;

	public List<QuizInfo> quiz {
		get {
			return _quiz;
		}
		set {
			_quiz = value;
		}
	}
}
