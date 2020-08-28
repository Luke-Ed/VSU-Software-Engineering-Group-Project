package laboratory;
//
import java.util.Comparator;

public class FormulaComparator implements Comparator<CompoundElement> {
    public int compare (CompoundElement compound1, CompoundElement compound2){
        return compound1.getFormula().compareTo(compound2.getFormula());
    }
}