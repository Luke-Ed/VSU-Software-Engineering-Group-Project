package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class CompoundElementTest {

    @Test
    @DisplayName("Test compute formula with ordered elements")
    void testComputeFormula_OrderedElements() {
        // ArrayList to hold the elements for the Water compound
        ArrayList<Element> elements = new ArrayList<>();

        // Create and add Hydrogen and Nitrogen to elements (Water)
        Element hydrogen = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxygen = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements.add(hydrogen);
        elements.add(oxygen);

        // Create Water compound
        CompoundElement compound = new CompoundElement("Water", elements);

        // Create actual formula name
        String water_Formula = "H2O";

        // Assert that the compound of 2 Hydrogen and 1 oxygen elements equals the water_Formula: H2O
        assertEquals(water_Formula, compound.getFormula());
    }

    @Test
    @DisplayName("Test compute formula with unordered elements")
    void testComputeFormula_UnorderedElements() {
        // ArrayList to hold the elements for the Water compound
        ArrayList<Element> elements = new ArrayList<>();

        // Create and add Oxygen and Hydrogen to elements (Water)
        Element oxygen = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        Element hydrogen = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        elements.add(oxygen);
        elements.add(hydrogen);

        // Create Water compound
        CompoundElement compound = new CompoundElement("Water", elements);

        // Create actual formula name
        String water_Formula = "H2O";

        // Assert that the compound of 1 oxygen and 2 Hydrogen elements equals the water_Formula: H2O
        assertEquals(water_Formula, compound.getFormula());
    }

    @Test
    void testComputeMolecularMass() {
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

        // Actual Molecular Mass of Water and Ammonia
        double water_MolMass = 18.0152;
        double ammonia_MolMass = 17.0304;

        // Assert that the actual Molecular Mass of Water and Ammonia equals the value returned from getMolecularMass()
        assertAll(() -> assertEquals(water_MolMass, compound_1.getMolecularMass()),
                  () -> assertEquals(ammonia_MolMass, compound_2.getMolecularMass()));
    }
}