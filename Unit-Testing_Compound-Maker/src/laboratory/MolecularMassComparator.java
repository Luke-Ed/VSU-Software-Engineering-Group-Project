package laboratory;
//
import java.util.Comparator;

public class MolecularMassComparator implements Comparator<CompoundElement>{
    public int compare (CompoundElement compound1, CompoundElement compound2){
//        if (compound1.getMolecularMass() > compound2.getMolecularMass()) return 1;
//        if (compound2.getMolecularMass() < compound2.getMolecularMass()) return -1;
//        else return 0;
    	return compound1.getMolecularMass().compareTo(compound2.getMolecularMass());
    }
}