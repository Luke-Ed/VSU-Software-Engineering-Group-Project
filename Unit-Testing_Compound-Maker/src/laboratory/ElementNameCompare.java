package laboratory;
//
import java.util.Comparator;

class ElementNameCompare implements Comparator <Element> {
	public int compare(Element e1, Element e2) {
		return e1.getName().compareTo(e2.getName());
	}
}