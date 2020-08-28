package laboratory;
//
import java.util.Comparator;


class CompoundNameComparator implements Comparator <CompoundElement> {
	public int compare(CompoundElement cE1, CompoundElement cE2) {
		return cE1.getName().compareTo(cE2.getName());
	}
}