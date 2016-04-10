
/*
  수레 정보.
  수레에 담겨있는 아이템관리.

  * 추후
  개별적인 업그레이드나 추가적인 수레의 기능
  개별적인 물리적 특성 (질량, 부피, 마찰)적용.
*/

public class Handcart {
	public int code { get; private set; }

	public int fushCount;

	public Handcart( int code ) {
		this.code = code;
	}

}
