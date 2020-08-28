package laboratory;
//
import java.util.Comparator;

public class AtomicNumberComparator implements Comparator<Element> {
	public int compare(Element e1, Element e2) {
		return e1.getAtomicNumber().compareTo(e2.getAtomicNumber());
	}
}
