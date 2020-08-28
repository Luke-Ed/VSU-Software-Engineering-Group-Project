package laboratory;
//
public enum Group {

		ALKALI_METAL("Alkali Metal", 1 ), 
		ALKALINE_EARTH_METAL("Alkaline Earth Metal", 2), 
	    LANTHANOID("Lanthanoid", 3),
	    ACTINOID("Actinoid", 4),
	    TRANSITION_METAL("Transition Metal", 5),
	    POST_TRANSITION_METAL("Post Transition Metal",6),
	    METALLOID("Metalloid", 7),
	    REACTIVE_NONMETAL("Reactive Nonmetal", 8),
	    NOBLE_GAS("Noble Gas", 9),
	    UNKNOWN("Unknown", 10);
		
		private final String group;
		
		Group(String string, int i) {
			this.group = string;
		}
	    
	    public String getGroup() {
			return group;
		}
	    
		@Override
	    public String toString() {return name().replace("_"," ");}
}
