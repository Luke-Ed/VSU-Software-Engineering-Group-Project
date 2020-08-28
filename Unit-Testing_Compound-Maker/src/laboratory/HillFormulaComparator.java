package laboratory;
//
import java.util.Comparator;

public class HillFormulaComparator implements Comparator<CompoundElement> {
    public int compare(CompoundElement compound1, CompoundElement compound2){
        return compound1.getHillFormula().compareTo(compound2.getHillFormula());
    }
}
