package laboratory;

import org.junit.jupiter.api.DisplayName;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

import static org.junit.jupiter.api.Assertions.*;

class CompoundNameComparatorTest {

    @Test
    @DisplayName("Test Compound Name Greater Than")
    void testCompare_CompoundNameGreaterThan() {
        ArrayList<Element> elements1 = new ArrayList<Element>();
        ArrayList<Element> elements2 = new ArrayList<Element>();

        Element hydro2 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 2);
        Element oxyg = new Element(8, "Oxygen", "O", 15.9994, 3, 8, 1);
        elements1.add(hydro2);
        elements1.add(oxyg);

        Element hydro1 = new Element(1, "Hydrogen", "H", 1.0079, 3, 8, 3);
        Element nitro = new Element(7, "Nitrogen", "N", 14.0067, 3, 8, 1);
        elements2.add(hydro1);
        elements2.add(nitro);

        CompoundElement compound1 = new CompoundElement("Water", elements1);
        CompoundElement compound2 = new CompoundElement("NH3", elements2);

        CompoundNameComparator cnc = new CompoundNameComparator();

        assertTrue(cnc.compare(compound1, compound2) > 0);
    }

    @Test
    @DisplayName("Test Compound Name Less Than")
    void testCompare_CompoundNameLessThan() {

    }

    @Test
    @DisplayName("Test Compound Name Equal To")
    void testCompare_CompoundNameEqualTo() {

    }
}