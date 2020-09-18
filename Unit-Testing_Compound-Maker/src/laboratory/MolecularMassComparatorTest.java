package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class MolecularMassComparatorTest {

    @Test
    @DisplayName("Test Molecular Mass Greater than")
    void testCompare_GreaterThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Oxygen to elements_1 (Water)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E1 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(oxygen_E1);

        // Create and add Hydrogen and Nitrogen to elements_2 (Ammonia)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitrogen_E2 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(nitrogen_E2);

        // Create compound_1 (Water) and compound_2 (Ammonia)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("Ammonia", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 molecular mass
        MolecularMassComparator mmc = new MolecularMassComparator();

        // If the molecular mass of compound_1 is greater than the molecular mass of compound_2 then the
        // comparator will return a value greater than 0.

        // Assert that the molecular mass of Water is greater than the molecular mass of Ammonia
        // compound_1 : Molecular Mass (Water)   : 18.0152
        // compound_2 : Molecular Mass (Ammonia) : 17.0304
        assertTrue(mmc.compare(compound_1, compound_2) > 0);
    }

    @Test
    @DisplayName("Test Molecular Mass Less than")
    void testCompare_LessThan() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Nitrogen to elements_1 (Ammonia)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitrogen_E1 = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(nitrogen_E1);

        // Create and add Hydrogen and Oxygen to elements_2 (Water)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E2 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(oxygen_E2);

        // Create compound_1 (Ammonia) and compound_2 (Water)
        CompoundElement compound_1 = new CompoundElement("Ammonia", elements_1);
        CompoundElement compound_2 = new CompoundElement("Water", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 by molecular mass
        MolecularMassComparator mmc = new MolecularMassComparator();

        // If the molecular mass of compound_1 is less than the molecular mass of compound_2 then the
        // comparator will return a value less than 0.

        // Assert that the molecular mass of Ammonia is less than the molecular mass of Water
        // compound_1 : Molecular Mass (Ammonia) : 17.0304
        // compound_2 : Molecular Mass (Water)   : 18.0152
        assertTrue(mmc.compare(compound_1, compound_2) < 0);
    }

    @Test
    @DisplayName("Test Molecular Mass Equal To")
    void testCompare_EqualTo() {
        // ArrayLists to hold the elements for each compound
        ArrayList<Element> elements_1 = new ArrayList<>();
        ArrayList<Element> elements_2 = new ArrayList<>();

        // Create and add Hydrogen and Oxygen to elements_1 (Water)
        Element hydrogen_E1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E1 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_1.add(hydrogen_E1);
        elements_1.add(oxygen_E1);

        // Create and add Hydrogen and Oxygen to elements_2 (Water)
        Element hydrogen_E2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen_E2 = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements_2.add(hydrogen_E2);
        elements_2.add(oxygen_E2);

        // Create compound_1 (Water) and compound_2 (Water)
        CompoundElement compound_1 = new CompoundElement("Water", elements_1);
        CompoundElement compound_2 = new CompoundElement("Water", elements_2);

        // Create the comparator object to compare compound_1 and compound_2 molecular mass
        MolecularMassComparator mmc = new MolecularMassComparator();

        // If the molecular mass of compound_1 is equal to the molecular mass of compound_2 then the
        // comparator will return a value equal to 0.

        // Assert that the molecular mass of Water is equal to the molecular mass of Water
        // compound_1 : Molecular Mass (Water)   : 18.0152
        // compound_2 : Molecular Mass (Water)   : 18.0152
        assertEquals(mmc.compare(compound_1, compound_2), 0);
    }
}