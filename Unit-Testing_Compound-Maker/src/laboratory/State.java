package laboratory;
//
public enum State {
	SOLID("Solid", 1),
	LIQUID("Liquid", 2),
	GAS("Gas", 3),
	UNKNOWN("Unknown", 4);
	
	private final String state;

	State(String string, int i) {
		this.state = string;
	}
	
	public String getState() {
		return state;
	}

}
