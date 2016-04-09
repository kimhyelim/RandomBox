using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// 수량 중첩이 되는 아이템.
public class Item {
	public int code; // 아이템 코드.
	public int count; // 현재 개수.

	public Item( int code, int count ) {
		this.code = code;
		this.count = count;
	}
}


// 플레이어의 상태.
public enum PlayerState {
	Walk, // 걷는중.
	Drive, // 수송중.
	Talk // npc와 대화중.
}


/* 전역적인 게임 매니저.
 * 플레이어의 정보, 아이템 등을 가짐.
 * 추후 플레이어 정보는 클래스로 따로 분리할 예정.
 */
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
	
	public int money;

	public Item[] items;

	public List< Handcart> handcarts = new List<Handcart>();

	public void init() {
		items = new Item[5];
		money = 1000;

		handcarts.Add(new Handcart(0));
		handcarts.Add(new Handcart(0));
		handcarts.Add(new Handcart(0));
	}


	// 아이템 관리 코드들.

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

}
