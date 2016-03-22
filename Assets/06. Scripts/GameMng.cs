using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item {
	public int code;
	public int count;

	public Item( int code, int count ) {
		this.code = code;
		this.count = count;
	}
}

public enum PlayerState {
	Walk, Drive, Talk
}


public class GameMng {
	private static GameMng inst;
	public static GameMng Inst {
		get {
			if ( inst == null ) {
				inst = new GameMng();
				inst.init();
			}
			return inst;
		}
	}

	public PlayerState state;
	public int money;

	public Item[] items;

	public void init() {
		items = new Item[5];
		money = 1000;
		state = PlayerState.Walk;
	}

	public void addItem(Item item) {
		for ( int i = 0, imax = items.Length ; i < imax ; ++i ) {
			if ( items[i] == null ) {
				items[i] = item;
			}
		}
	}

	public void removeItem(int code) {
		for ( int i = 0, imax = items.Length ; i < imax ; ++i ) {
			if ( items[i] != null && items[i].code == code ) {
				items[i] = null;
			}
		}
	}

	public Item getItem(int code) {
		for ( int i = 0, imax = items.Length ; i < imax ; ++i ) {
			if ( items[i] != null && items[i].code == code ) {
				return items[i];
			}
		}
		return null;
	}

	//public void setState( PlayerState state ) {
	//	this.state = state;
	//}
}
