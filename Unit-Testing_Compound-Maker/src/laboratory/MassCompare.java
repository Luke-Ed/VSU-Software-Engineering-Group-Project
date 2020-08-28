package laboratory;
//
import java.util.Comparator;

class MassCompare implements Comparator <Element> {
	public int compare(Element e1, Element e2) {
//		if(e1.getAtomicMass() > e2.getAtomicMass()) return 1;
//		if(e1.getAtomicMass() < e2.getAtomicMass()) return -1;
//		else return 0;
		return e1.getAtomicMass().compareTo(e2.getAtomicMass());
	}
}
