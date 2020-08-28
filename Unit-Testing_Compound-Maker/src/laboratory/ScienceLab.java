package laboratory;

import java.util.ArrayList;

public class ScienceLab {
	ArrayList <Element> compounds = new ArrayList<Element>();
	public static void main(String [] args) {
		PeriodicTable p = new PeriodicTable();
		p.sortBySymbol();
		p.printPeriodicTable();
		p.sortByMass();
		p.printPeriodicTable();
		p.sortByName();
		p.printPeriodicTable();
		p.sortByAtomicNumber();
		p.printPeriodicTable();
		
	}
}


