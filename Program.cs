using System.Text;

internal class Program {
    static void Main(string[] args) {

        char[] dnaStrand1 = new char[2000];
        char[] dnaStrand2 = new char[2000];
        int geneLinePreference = 0;

        Random random = new Random();

        //  Sets all of the characters to ' ' in the desired strand
        void ClearDNAStrand(char[] dnaStrand) {
            for (int i = 0; i < dnaStrand.Length; i++) {
                dnaStrand[i] = ' ';
            }
        }

        //  Calculates nucleotide count of the given dna
        int GetNucleotideCount(char[] dna) {
            int sum = 0;

            for (int i = 0; i < dna.Length; i++) {
                if (dna[i] == 'A' || dna[i] == 'T' || dna[i] == 'G' || dna[i] == 'C') {
                    sum++;
                }
            }

            return sum;
        }

        //  Writes DNA strand to console with spaces
        //  lineBetweenGenes determines if genes will be in the same line or not
        void WriteDNA(char[] dna, int lineBetweenGenes) {
            char firstNucleotide, secondNucleotide, thirdNucleotide;

            if (lineBetweenGenes == 1) {
                Console.WriteLine();
            }

            for (int i = 0; i < GetNucleotideCount(dna); i += 3) {
                firstNucleotide = dna[i];
                secondNucleotide = dna[i + 1];
                thirdNucleotide = dna[i + 2];

                if ((firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'G' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'G')) {
                    Console.ForegroundColor = ConsoleColor.Red;
                }

                if (firstNucleotide == 'A' && secondNucleotide == 'T' && thirdNucleotide == 'G') {
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                //  Writes current codon and a space to console 
                Console.Write(firstNucleotide);
                Console.Write(secondNucleotide);
                Console.Write(thirdNucleotide);
                Console.Write(' ');

                Console.ResetColor();

                //  Adds a new line if current codon is a stop codon
                if (((firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'G' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'G')) && lineBetweenGenes > 0) {
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
        }

        //  Sets dnaStrand1 from the given txt file
        void Operation1(string fileName) {

            //  Clears DNA strand 1 in a case it has already been used
            ClearDNAStrand(dnaStrand1);

            //  Reads all text from the specified file
            string tempText = File.ReadAllText(fileName, Encoding.UTF8);

            //  Places the text from the file to DNA strand 1
            Operation2(tempText);
        }

        //  Sets dnaStrand1 from a string
        void Operation2(string strDNA) {

            //  Clears DNA strand 1 in a case it has already been used
            ClearDNAStrand(dnaStrand1);

            //  Places each of the characters to DNA strand 1 (char array)
            for (int i = 0; i < strDNA.Length; i++) {
                dnaStrand1[i] = strDNA[i];
            }
        }

        //  Creates a random BLOB dna with gender parameter and places it in the desired DNA strand
        char[] Operation3(char gender) {

            char[] tempDNA = new char[2000];
            char[] nucleotides = new char[] { 'A', 'T', 'G', 'C' };
            char firstGender, secondGender, firstNucleotide, secondNucleotide, thirdNucleotide;

            int randomGenes, randomCodons, totalCodonCount;


            //  Determines the gender codons randomly (gender is decided by gender parameter)
            if (gender == 'f') {
                firstGender = nucleotides[random.Next(0, 2)];
                secondGender = nucleotides[random.Next(0, 2)];
            }
            else {
                firstGender = nucleotides[random.Next(0, 4)];

                if (firstGender == 'A' || firstGender == 'T') {
                    secondGender = nucleotides[random.Next(2, 4)];
                }
                else {
                    secondGender = nucleotides[random.Next(0, 2)];
                }
            }

            //  Places the first gene into temporary char array
            tempDNA[0] = 'A';
            tempDNA[1] = 'T';
            tempDNA[2] = 'G';

            tempDNA[3] = firstGender;
            tempDNA[4] = firstGender;
            tempDNA[5] = firstGender;

            tempDNA[6] = secondGender;
            tempDNA[7] = secondGender;
            tempDNA[8] = secondGender;

            tempDNA[9] = 'T';
            tempDNA[10] = 'A';
            tempDNA[11] = 'G';

            //  Updates the total codon count
            totalCodonCount = 4;

            //  Creates a random number to determine gene count of the random DNA sequence
            randomGenes = random.Next(1, 7);

            //  Loops according to randomGenes variable
            for (int i = 0; i < randomGenes; i++) {

                //  Places start codon after the last one
                tempDNA[totalCodonCount * 3 + 0] = 'A';
                tempDNA[totalCodonCount * 3 + 1] = 'T';
                tempDNA[totalCodonCount * 3 + 2] = 'G';

                //  Randomly decides codon count in the current gene
                randomCodons = random.Next(1, 7);

                //  Randomly decides the codons
                for (int j = 0; j < randomCodons; j++) {

                    //  Recreates the codon if it is a start or a stop codon
                    do {
                        firstNucleotide = nucleotides[random.Next(0, 4)];
                        secondNucleotide = nucleotides[random.Next(0, 4)];
                        thirdNucleotide = nucleotides[random.Next(0, 4)];
                    }
                    while ((firstNucleotide == 'A' && secondNucleotide == 'T' && thirdNucleotide == 'G') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'G' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'G'));

                    tempDNA[(totalCodonCount + j) * 3 + 3] = firstNucleotide;
                    tempDNA[(totalCodonCount + j) * 3 + 4] = secondNucleotide;
                    tempDNA[(totalCodonCount + j) * 3 + 5] = thirdNucleotide;
                }


                //  Randomizes the stop codon
                firstNucleotide = 'T';
                switch (random.Next(0, 3)) {
                    case 0:
                        secondNucleotide = 'A';
                        thirdNucleotide = 'A';
                        break;
                    case 1:
                        secondNucleotide = 'G';
                        thirdNucleotide = 'A';
                        break;
                    default:
                        secondNucleotide = 'A';
                        thirdNucleotide = 'G';
                        break;
                }

                //  Places stop codon after the last one
                tempDNA[(totalCodonCount + randomCodons + 1) * 3 + 0] = firstNucleotide;
                tempDNA[(totalCodonCount + randomCodons + 1) * 3 + 1] = secondNucleotide;
                tempDNA[(totalCodonCount + randomCodons + 1) * 3 + 2] = thirdNucleotide;

                //  Updates totalCodonCount value
                totalCodonCount += randomCodons + 2;
            }

            return tempDNA;
        }

        //  Checks the given DNA string for errors
        bool Operation4(char[] dna, int writeToConsole) {
            char firstNucleotide, secondNucleotide, thirdNucleotide;

            int nucleotideCount = GetNucleotideCount(dna);
            int structureCheck = 0;

            bool error = false;
            string output = "";

            for (int i = 0; i < nucleotideCount; i += 3) {
                firstNucleotide = dna[i];
                secondNucleotide = dna[i + 1];
                thirdNucleotide = dna[i + 2];

                //  structureCheck++ if current codon is a start codon, structureCheck-- if it is a stop codon
                if (firstNucleotide == 'A' && secondNucleotide == 'T' && thirdNucleotide == 'G') {
                    structureCheck++;
                }
                else if ((firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'G' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'G')) {
                    structureCheck--;
                }

                //  If it exceeds [0,1] range, it means there were consecutive start or stop codons
                //  Checks the first and last codons too in a case they are not start and stop codons
                if ((structureCheck == 2 || structureCheck == -1) ||
                    (i == 0 && !(firstNucleotide == 'A' && secondNucleotide == 'T' && thirdNucleotide == 'G')) ||
                    ((i == nucleotideCount - 3 && !(firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'A')) &&
                    (i == nucleotideCount - 3 && !(firstNucleotide == 'T' && secondNucleotide == 'G' && thirdNucleotide == 'A')) &&
                    (i == nucleotideCount - 3 && !(firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'G')))) {

                    //  Adds error to the output string
                    output += "Gene structure error. ";
                    error = true;

                    //  Updates structureCheck value so we do not get the same error twice
                    structureCheck = 10000;
                }
            }

            //  Checks if codon structure is OK
            if (nucleotideCount % 3 != 0) {
                output += "Codon structure error.";
                error = true;
            }

            //  Checks if it is a BLOB DNA and its structure is ok
            if (Operation5(dna, 0) && output == "") {
                output = "Gene structure is OK. (Not BLOB DNA, but OK)";
            }
            else if (output == "") {
                output = "Gene structure is OK";
            }

            if (writeToConsole != 0) {
                //  Prints DNA strand and the result to console if desired
                Console.Write("DNA Strand : ");
                WriteDNA(dna, geneLinePreference);

                Console.WriteLine(output);
            }

            //  Returns true if the DNA strand had structure errors
            return error;
        }

        //  Checks if the DNA belongs to a BLOB and writes error messages if desired
        bool Operation5(char[] dna, int writeToConsole) {
            char firstNucleotide, secondNucleotide, thirdNucleotide;
            char gender1 = 'E';
            char gender2 = 'E';

            int NucleotideCount = GetNucleotideCount(dna);
            int currentCodonCount = 0;
            int geneCount = 0;

            bool error = false;
            string output = "";

            //  Loops for the amount of codons in the dna
            for (int i = 0; i < NucleotideCount; i += 3) {
                firstNucleotide = dna[i];
                secondNucleotide = dna[i + 1];
                thirdNucleotide = dna[i + 2];

                //  Increments codon count each loop
                currentCodonCount++;

                //  Checks if the current codon is start codon or the last codon
                //  It basically counts start codons to determine geneCount and codonCount in each gene
                if ((firstNucleotide == 'A' && secondNucleotide == 'T' && thirdNucleotide == 'G') || i == NucleotideCount - 3) {

                    //  Increments currentCodonCount in case current codon is the last one
                    //  else geneCount++
                    if (i == NucleotideCount - 3) {
                        currentCodonCount++;
                    }
                    else {
                        geneCount++;
                    }

                    //  Checks current codon count and gives an error if it is needed
                    //  This step is skipped if current codon is the first codon
                    if ((currentCodonCount < 3 || currentCodonCount > 8) && i != 0) {
                        output += ("Number of codons error. " + currentCodonCount);
                        error = true;
                    }

                    //  Resets the currentCodonCount
                    currentCodonCount = 0;
                }

                //  Checks if second and third codons are gender codons and assigns chromosome letters to gender1 and gender2
                if ((i == 3) && (firstNucleotide == secondNucleotide && firstNucleotide == thirdNucleotide)) {
                    if (firstNucleotide == 'A' || firstNucleotide == 'T') {
                        gender1 = 'X';
                    }
                    else {
                        gender1 = 'Y';
                    }
                }
                else if ((i == 6) && (firstNucleotide == secondNucleotide && firstNucleotide == thirdNucleotide)) {
                    if (firstNucleotide == 'A' || firstNucleotide == 'T') {
                        gender2 = 'X';
                    }
                    else {
                        gender2 = 'Y';
                    }
                }
            }

            //  Checks gene count and gives an error if needed
            if (geneCount < 2 || geneCount > 8) {
                output += "Number of genes error. ";
                error = true;
            }

            //  Checks gender chromosomes and gives an error if needed
            if ((gender1 == 'Y' && gender2 == 'Y') || (gender1 == 'E' || gender2 == 'E')) {
                output += "Gender error.";
                error = true;
            }

            //  If there was no error change the output
            if (!error) {
                output = "BLOB is OK.";
            }

            //  Prints DNA strand and the result to console if desired
            if (writeToConsole > 0) {
                Console.Write("DNA Strand : ");
                WriteDNA(dna, geneLinePreference);

                Console.WriteLine(output);
            }

            //  Returns true if the DNA strand did not belong to a BLOB
            return error;
        }

        //  Takes complement of the DNA
        char[] Operation6(char[] dna) {

            char[] newDna = new char[dna.Length];

            for (int i = 0; i < GetNucleotideCount(dna); i++) {
                switch (dna[i]) {
                    case 'A':
                        newDna[i] = 'T';
                        break;
                    case 'T':
                        newDna[i] = 'A';
                        break;
                    case 'G':
                        newDna[i] = 'C';
                        break;
                    default:
                        newDna[i] = 'G';
                        break;
                }
            }

            //  Prints DNA strand and the result to console if desired
            Console.Write("DNA Strand : ");
            WriteDNA(dna, geneLinePreference);

            Console.Write("Complement : ");
            WriteDNA(newDna, geneLinePreference);

            return newDna;
        }

        //  Determines the aminoacids
        string Operation7(char[] dna) {
            string aminoAcids = "";

            for (int i = 0; i < GetNucleotideCount(dna); i += 3) {
                switch (Convert.ToString(dna[i]) + dna[i + 1] + dna[i + 2]) {
                    case "GCT":
                    case "GCC":
                    case "GCA":
                    case "GCG":
                        aminoAcids += "Ala ";
                        break;
                    case "CGT":
                    case "CGC":
                    case "CGA":
                    case "CGG":
                    case "AGA":
                    case "AGG":
                        aminoAcids += "Arg ";
                        break;
                    case "AAT":
                    case "AAC":
                        aminoAcids += "Asn ";
                        break;
                    case "GAT":
                    case "GAC":
                        aminoAcids += "Asp ";
                        break;
                    case "TGT":
                    case "TGC":
                        aminoAcids += "Cys ";
                        break;
                    case "CAA":
                    case "CAG":
                        aminoAcids += "Gln ";
                        break;
                    case "GAA":
                    case "GAG":
                        aminoAcids += "Glu ";
                        break;
                    case "GGT":
                    case "GGC":
                    case "GGA":
                    case "GGG":
                        aminoAcids += "Gly ";
                        break;
                    case "CAT":
                    case "CAC":
                        aminoAcids += "His ";
                        break;
                    case "ATT":
                    case "ATC":
                    case "ATA":
                        aminoAcids += "Ile ";
                        break;
                    case "CTT":
                    case "CTC":
                    case "CTA":
                    case "CTG":
                    case "TTA":
                    case "TTG":
                        aminoAcids += "Leu ";
                        break;
                    case "AAA":
                    case "AAG":
                        aminoAcids += "Lys ";
                        break;
                    case "ATG":
                        aminoAcids += "Met ";
                        break;
                    case "TTT":
                    case "TTC":
                        aminoAcids += "Phe ";
                        break;
                    case "CCT":
                    case "CCC":
                    case "CCA":
                    case "CCG":
                        aminoAcids += "Pro ";
                        break;
                    case "TCT":
                    case "TCC":
                    case "TCA":
                    case "TCG":
                    case "AGT":
                    case "AGC":
                        aminoAcids += "Ser ";
                        break;
                    case "ACT":
                    case "ACC":
                    case "ACA":
                    case "ACG":
                        aminoAcids += "Thr ";
                        break;
                    case "TGG":
                        aminoAcids += "Trp ";
                        break;
                    case "TAT":
                    case "TAC":
                        aminoAcids += "Try ";
                        break;
                    case "GTT":
                    case "GTC":
                    case "GTA":
                    case "GTG":
                        aminoAcids += "Val ";
                        break;
                    case "TAA":
                    case "TGA":
                    case "TAG":
                        aminoAcids += Convert.ToString(dna[i]) + dna[i + 1] + dna[i + 2] + " ";
                        break;
                }
            }
            Console.Write("DNA Strand  : ");
            WriteDNA(dna, geneLinePreference);

            Console.Write("Amino acids : ");
            Console.WriteLine(aminoAcids);

            return aminoAcids;
        }

        //  Deletes n codons starting from m(th) codon
        char[] Operation8(char[] dna, int m, int n) {
            char[] newDna = new char[dna.Length];

            //  This does not include the codons that will be deleted
            for (int i = 0; i < GetNucleotideCount(dna); i++) {
                if (i < 3 * (m - 1)) {
                    newDna[i] = dna[i];
                }
                else if (i > 3 * (m + n - 1) - 1) {
                    newDna[i - 3 * n] = dna[i];
                }
            }

            //  Prints DNA strand and the result to console if desired
            Console.Write("DNA strand (stage 1) : ");
            WriteDNA(dna, geneLinePreference);

            Console.WriteLine("Delete {0} codons, starting from codon {1}.", n, m);

            Console.Write("DNA strand (stage 2) : ");
            WriteDNA(newDna, geneLinePreference);

            return newDna;
        }

        //  Inserts n codons starting from m(th) codon
        char[] Operation9(char[] dna, char[] codonSequence, int m) {
            char[] newDna = new char[dna.Length];
            int n = GetNucleotideCount(codonSequence) / 3;

            //  It inserts when the desired codon's starting index is in the range of i
            for (int i = 0; i < GetNucleotideCount(dna) + GetNucleotideCount(codonSequence); i++) {
                if (i < 3 * (m - 1)) {
                    newDna[i] = dna[i];
                }
                else if (i >= 3 * (m - 1) && i <= 3 * (m + n - 1) - 1) {
                    newDna[i] = codonSequence[i - 3 * (m - 1)];
                }
                else {
                    newDna[i] = dna[i - 3 * n];
                }
            }

            return newDna;
        }

        //  Detects if there is at least 1 sequence of codons that is identical with codonSequence
        int Operation10(char[] dna, char[] codonSequence, int m) {
            int n = GetNucleotideCount(codonSequence);
            int result = -1;
            bool error = false;
            char[] tfList = new char[n];

            if (n > GetNucleotideCount(dna)) {
                return -1;
            }

            for (int i = 0; i < GetNucleotideCount(dna); i++) {
                if (i >= 3 * (m - 1) && i % 3 == 0) {

                    //  Checks the nucleotids and adds 'T' if they are the same, 'F' if not
                    for (int j = 0; j < n; j++) {

                        if (dna[i + j] == codonSequence[j]) {
                            tfList[j] = 'T';
                        }
                        else {
                            tfList[j] = 'F';
                        }

                    }

                    //  Checks if all tfList items are T
                    error = false;
                    for (int j = 0; j < tfList.Length; j++) {
                        if (tfList[j] == 'F') {
                            error = true;
                        }
                    }

                    if (!error) {
                        result = i / 3 + 1;
                        i = GetNucleotideCount(dna);
                    }
                }
            }

            if (result != -1 && GetNucleotideCount(codonSequence) % 3 == 0) {
                Console.WriteLine("Result         : " + result);
            }
            else {
                Console.WriteLine("Result         : -1 (Not found)");
            }

            return result;
        }

        //  Reverses n codons starting from m
        char[] Operation11(char[] dna, int n, int m) {
            char[] newDna = new char[dna.Length];
            for (int i = 0; i < GetNucleotideCount(dna); i += 3) {

                if (i >= (m - 1) * 3 && i <= (m + n - 1) * 3 - 1) {

                    newDna[i] = dna[(m + n - 1) * 3 - i + ((m - 2) * 3)];
                    newDna[i + 1] = dna[(m + n - 1) * 3 - i + 1 + ((m - 2) * 3)];
                    newDna[i + 2] = dna[(m + n - 1) * 3 - i + 2 + ((m - 2) * 3)];


                }
                else {
                    newDna[i] = dna[i];
                    newDna[i + 1] = dna[i + 1];
                    newDna[i + 2] = dna[i + 2];
                }
            }

            return newDna;
        }

        int Operation12(char[] dna) {
            char firstNucleotide, secondNucleotide, thirdNucleotide;

            int nucleotideCount = GetNucleotideCount(dna);
            int structureCheck = 0;

            for (int i = 0; i < nucleotideCount; i += 3) {
                firstNucleotide = dna[i];
                secondNucleotide = dna[i + 1];
                thirdNucleotide = dna[i + 2];

                //  structureCheck++ if current codon is a start codon, structureCheck-- if it is a stop codon
                if (firstNucleotide == 'A' && secondNucleotide == 'T' && thirdNucleotide == 'G') {
                    structureCheck++;
                }
            }

            if (Operation4(dna, 0)) {
                structureCheck = -1;
            }

            //  Returns true if the DNA strand had structure errors
            return structureCheck;
        }

        int Operation13(char[] dna) {
            char firstNucleotide, secondNucleotide, thirdNucleotide;
            char[] newDna = new char[dna.Length];
            int nucleotideCount = GetNucleotideCount(dna);
            int structureCheck = 0;
            int oldAtgIndex = 0;
            int AtgIndex = 0;
            int codonCount = 0;
            int smallestCodonCount = 10000;


            for (int i = 0; i < nucleotideCount; i += 3) {
                firstNucleotide = dna[i];
                secondNucleotide = dna[i + 1];
                thirdNucleotide = dna[i + 2];

                //  structureCheck++ if current codon is a start codon, structureCheck-- if it is a stop codon
                if (firstNucleotide == 'A' && secondNucleotide == 'T' && thirdNucleotide == 'G') {
                    AtgIndex = i;
                    structureCheck++;
                    codonCount++;
                }
                else if ((firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'G' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'G')) {
                    structureCheck--;
                    if (codonCount < smallestCodonCount) {
                        smallestCodonCount = codonCount;
                        oldAtgIndex = AtgIndex;

                    }
                    else {
                        AtgIndex = oldAtgIndex;
                    }
                    codonCount = 0;
                }
                else {
                    if (codonCount > 0) {
                        codonCount++;
                    }
                }

            }

            for (int k = 0; k < (smallestCodonCount + 1) * 3; k++) {
                newDna[k] = dna[AtgIndex + k];
            }

            Console.Write("Shortest gene                : ");
            WriteDNA(newDna, geneLinePreference);

            Console.WriteLine("Number of codons in the gene : " + (smallestCodonCount + 1));
            Console.WriteLine("Position of the gene         : " + (AtgIndex / 3 + 1));

            return smallestCodonCount + 1;
        }

        int Operation14(char[] dna) {
            char firstNucleotide, secondNucleotide, thirdNucleotide;
            char[] newDna = new char[dna.Length];
            int nucleotideCount = GetNucleotideCount(dna);
            int structureCheck = 0;
            int oldAtgIndex = 0;
            int AtgIndex = 0;
            int codonCount = 0;
            int longestCodonCount = -10000;


            for (int i = 0; i < nucleotideCount; i += 3) {
                firstNucleotide = dna[i];
                secondNucleotide = dna[i + 1];
                thirdNucleotide = dna[i + 2];

                //  structureCheck++ if current codon is a start codon, structureCheck-- if it is a stop codon
                if (firstNucleotide == 'A' && secondNucleotide == 'T' && thirdNucleotide == 'G') {
                    AtgIndex = i;
                    structureCheck++;
                    codonCount++;
                }
                else if ((firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'G' && thirdNucleotide == 'A') ||
                    (firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'G')) {
                    structureCheck--;
                    if (codonCount > longestCodonCount) {
                        longestCodonCount = codonCount;
                        oldAtgIndex = AtgIndex;

                    }
                    else {
                        AtgIndex = oldAtgIndex;
                    }
                    codonCount = 0;
                }
                else {
                    if (codonCount > 0) {
                        codonCount++;
                    }
                }

            }

            for (int k = 0; k < (longestCodonCount + 1) * 3; k++) {
                newDna[k] = dna[AtgIndex + k];
            }

            Console.Write("Longest gene                 : ");
            WriteDNA(newDna, geneLinePreference);

            Console.WriteLine("Number of codons in the gene : " + (longestCodonCount + 1));
            Console.WriteLine("Position of the gene         : " + (AtgIndex / 3 + 1));

            return longestCodonCount + 1;
        }

        void Operation15(char[] dna, int nucleotideCount) {
            int currentGreatest = 0, totalGreatest = 0;
            int greatestIndex = 0, oldGreatestIndex = 0;
            int lastIndex = -10000;
            bool error = false;
            char[] tfList = new char[nucleotideCount];
            char[] charSequence = new char[nucleotideCount];

            for (int i = 0; i < GetNucleotideCount(dna) - nucleotideCount + 1; i++) {

                currentGreatest = 0;
                greatestIndex = i;
                for (int j = 0; j < nucleotideCount; j++) {
                    charSequence[j] = dna[i + j];
                }
                for (int k = 0; k < GetNucleotideCount(dna) - nucleotideCount + 1; k++) {
                    for (int l = 0; l < nucleotideCount; l++) {
                       
                        if (dna[k + l] == charSequence[l]) {
                            tfList[l] = 'T';
                        }
                        else {
                            tfList[l] = 'F';
                        }
                    }

                    error = false;
                    for (int j = 0; j < tfList.Length; j++) {
                        if (tfList[j] == 'F') {
                            error = true;
                        }
                    }

                    if (!error) {
                        currentGreatest++;
                    }
                }

                if (currentGreatest > totalGreatest) {
                    totalGreatest = currentGreatest;
                    oldGreatestIndex = greatestIndex;
                }
                else {
                    currentGreatest = totalGreatest;
                    greatestIndex = oldGreatestIndex;
                }
            }

            Console.Write("Most repeated sequence : ");
            for (int i = greatestIndex; i < greatestIndex + nucleotideCount; i++) {
                Console.Write(dna[i]);
            }

            Console.WriteLine("\nFrequency              : " + currentGreatest);

        }

        int Operation16(char[] dna) {
            int h2 = 0;
            int h3 = 0;

            for (int i = 0; i < GetNucleotideCount(dna); i++) {
                switch (dna[i]) {
                    case 'A':
                    case 'T':
                        h2++;
                        break;
                    case 'G':
                    case 'C':
                        h3++;
                        break;

                }
            }

            Console.WriteLine("Number of pairing with 2-hydrogen bonds : " + h2);
            Console.WriteLine("Number of pairing with 3-hydrogen bonds : " + h3);
            Console.WriteLine("Number of hydrogen bonds : " + ((h2 * 2) + (h3 * 3)));

            return (h2 * 2) + (h3 * 3);
        }

        void Operation17(char[] dna1, char[] dna2) {
            char[] newDna = new char[2000];
            char firstNucleotide, secondNucleotide, thirdNucleotide, gender11, gender12, gender21, gender22;
            

            bool jBool = true;
            for (int j = 0; j < 10 && jBool; j++) {
                int smallerNucleotideCount;
                int index1 = 12, index2 = 12;
                int gcCount = 0;
                int consecutiveGCCount = 0;
                bool dnaSwitch = true;
                string output = "";

                if (GetNucleotideCount(dna1) < GetNucleotideCount(dna2)) {
                    smallerNucleotideCount = GetNucleotideCount(dna2);
                }
                else {
                    smallerNucleotideCount = GetNucleotideCount(dna1);
                }

                if (dna1[3] == 'A' || dna1[3] == 'T') {
                    gender11 = 'X';
                }
                else {
                    gender11 = 'Y';
                }

                if (dna1[6] == 'A' || dna1[6] == 'T') {
                    gender12 = 'X';
                }
                else {
                    gender12 = 'Y';
                }

                if (dna2[3] == 'A' || dna2[3] == 'T') {
                    gender21 = 'X';
                }
                else {
                    gender21 = 'Y';
                }

                if (dna2[6] == 'A' || dna2[6] == 'T') {
                    gender22 = 'X';
                }
                else {
                    gender22 = 'Y';
                }

                Console.WriteLine("Generation {0}:", j + 1);

                Console.Write("BLOB1-" + gender11 + gender12 + " : ");
                WriteDNA(dna1, geneLinePreference);

                Console.Write("BLOB2-" + gender21 + gender22 + " : ");
                WriteDNA(dna2, geneLinePreference);

                newDna[0] = 'A';
                newDna[1] = 'T';
                newDna[2] = 'G';

                newDna[3] = dna1[3];
                newDna[4] = dna1[4];
                newDna[5] = dna1[5];

                newDna[6] = dna2[6];
                newDna[7] = dna2[7];
                newDna[8] = dna2[8];

                newDna[9] = 'T';
                newDna[10] = 'A';
                newDna[11] = 'G';

                bool error = false;
                for (int i = 0; i < 10000000 && !error; i += 3) {
                   

                    if (index1 + 3 == GetNucleotideCount(dna1)) {
                        error = true;
                    }
                    if (index2 + 3 == GetNucleotideCount(dna2)) {
                        error = true;
                    }

                    if (dnaSwitch) {
                        firstNucleotide = dna1[index1];
                        secondNucleotide = dna1[index1 + 1];
                        thirdNucleotide = dna1[index1 + 2];
                        index1 += 3;

                    }
                    else {
                        firstNucleotide = dna2[index2];
                        secondNucleotide = dna2[index2 + 1];
                        thirdNucleotide = dna2[index2 + 2];
                        index2 += 3;
                    }

                    newDna[i + 12] = firstNucleotide;
                    newDna[i + 13] = secondNucleotide;
                    newDna[i + 14] = thirdNucleotide;

                    if ((firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'A') ||
                        (firstNucleotide == 'T' && secondNucleotide == 'G' && thirdNucleotide == 'A') ||
                        (firstNucleotide == 'T' && secondNucleotide == 'A' && thirdNucleotide == 'G')) {
                        if (dnaSwitch) {
                            dnaSwitch = false;
                            if (i == 0) {
                                index2 += i;
                            }
                        }
                        else {
                            dnaSwitch = true;
                        }
                    }
                }

                Console.Write("BLOB3-" + gender11 + gender22 + " : ");
                WriteDNA(newDna, geneLinePreference);

                consecutiveGCCount = 0;
                for (int k = 0; k < GetNucleotideCount(newDna); k += 3) {
                    char first = newDna[k];
                    char second = newDna[k + 1];
                    char third = newDna[k + 2];


                    if ((first == 'G' || first == 'C') && (second == 'G' || second == 'C') && (third == 'G' || third == 'C')) {
                        gcCount++;
                        consecutiveGCCount++;
                    }
                    else {
                        consecutiveGCCount = 0;
                    }
                    if (consecutiveGCCount == 3) {
                        output += "The BLOB3 has at least 3 consecutive 3-hydrogen bond codons. ";
                        jBool = false;
                    }
                }

                Console.WriteLine("BLOB3 faulty codons ratio = {0}/{1} = {2}%\n", gcCount, GetNucleotideCount(newDna) / 
                                          3, (double)gcCount / ((double)GetNucleotideCount(newDna) / 3) * 100.0);

                if ((double)gcCount / ((double)GetNucleotideCount(newDna) / 3) * 100.0 > 10.0) {
                    output += "Newborn dies. Generations are over.";
                    jBool = false;
                }

                Console.WriteLine(output);

                dna1 = newDna;

                if ((newDna[3] == 'A' || newDna[3] == 'T') && (newDna[6] == 'A' || newDna[6] == 'T')) {
                    dna2 = Operation3('m');
                }
                else {
                    dna2 = Operation3('f');
                }
            }


        }

        void GetInput() {
            Console.Write("Operation: ");

            string[] validInputList = new string[] { "exit", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17" };
            string operationNumber = Console.ReadLine();

            string stringCodonSequence;
            char[] codonSequence;

            int m, n;

            bool error = true;
            while (error) {
                for (int i = 0; i < validInputList.Length; i++) {
                    if (validInputList[i] == operationNumber) {
                        error = false;
                    }
                }
                if (error) {
                    Console.WriteLine("Input is invalid.");
                    for (int i = 0; i < Console.BufferWidth - 1; i++) {
                        Console.Write("─");
                    }
                    Console.WriteLine();
                    Console.Write("Operation: ");

                    operationNumber = Console.ReadLine();
                }
            }

            Console.WriteLine();

            switch (operationNumber) {
                case "1":
                    Console.Write("Please type in the file name you want to use: ");
                    string fileName = Console.ReadLine();

                    Operation1(fileName);

                    Console.Write(fileName + ": ");
                    WriteDNA(dnaStrand1, geneLinePreference);
                    break;

                case "2":
                    Console.Write("Please type in the DNA you want to use: ");
                    string dnaString = Console.ReadLine();

                    Operation2(dnaString);

                    Console.Write("DNA strand 1: ");
                    WriteDNA(dnaStrand1, geneLinePreference);
                    break;

                case "3":
                    Console.Write("BLOB gender (m or f) : ");
                    string gender = Console.ReadLine();

                    Console.Write("DNA strand           : ");
                    string dnaStrandNumber = Console.ReadLine();

                    if (dnaStrandNumber == "1") {
                        dnaStrand1 = Operation3(Convert.ToChar(gender));
                    }
                    else {
                        dnaStrand2 = Operation3(Convert.ToChar(gender));
                    }

                    Console.Write("DNA strand {0} : ", dnaStrandNumber);
                    if (dnaStrandNumber == "1") {
                        WriteDNA(dnaStrand1, geneLinePreference);
                    }
                    else {
                        WriteDNA(dnaStrand2, geneLinePreference);
                    }

                    break;

                case "4":
                    Operation4(dnaStrand1, 1);
                    break;

                case "5":
                    Operation5(dnaStrand1, 1);
                    break;

                case "6":
                    Operation6(dnaStrand1);
                    break;

                case "7":
                    Operation7(dnaStrand1);
                    break;

                case "8":
                    Console.Write("Number of codons that will be deleted: ");
                    n = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Starting codon number to start deleting: ");
                    m = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

                    dnaStrand1 = Operation8(dnaStrand1, m, n);
                    break;

                case "9":
                    Console.Write("DNA strand (stage 1) : ");
                    WriteDNA(dnaStrand1, geneLinePreference);

                    Console.Write("Codon sequence       : ");
                    stringCodonSequence = Console.ReadLine();
                    codonSequence = new char[2000];
                    for (int i = 0; i < stringCodonSequence.Length; i++) {
                        codonSequence[i] = stringCodonSequence[i];
                    }

                    Console.Write("Starting from        : ");
                    int startingCodonToInsert = Convert.ToInt32(Console.ReadLine());

                    dnaStrand1 = Operation9(dnaStrand1, codonSequence, startingCodonToInsert);

                    Console.Write("DNA strand (stage 2) : ");
                    WriteDNA(dnaStrand1, geneLinePreference);
                    break;

                case "10":
                    Console.Write("DNA strand     : ");
                    WriteDNA(dnaStrand1, geneLinePreference);

                    Console.Write("Codon sequence : ");
                    stringCodonSequence = Console.ReadLine();
                    codonSequence = new char[2000];
                    for (int i = 0; i < stringCodonSequence.Length; i++) {
                        codonSequence[i] = stringCodonSequence[i];
                    }

                    Console.Write("Starting from  : ");
                    int startingCodonToSearch = Convert.ToInt32(Console.ReadLine());

                    Operation10(dnaStrand1, codonSequence, startingCodonToSearch);
                    break;

                case "11":
                    Console.Write("DNA strand (stage 1) : ");
                    WriteDNA(dnaStrand1, geneLinePreference);

                    Console.Write("Starting from        : ");
                    m = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Number of Codons     : ");
                    n = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("\nReverse {0} codons, starting from codon {1}.", n, m);

                    dnaStrand1 = Operation11(dnaStrand1, n, m);

                    Console.Write("DNA strand (stage 2) : ");
                    WriteDNA(dnaStrand1, geneLinePreference);
                    break;

                case "12":
                    Console.Write("DNA strand      : ");
                    WriteDNA(dnaStrand1, geneLinePreference);

                    Console.Write("Number of genes : " + Operation12(dnaStrand1));
                    if (Operation12(dnaStrand1) == -1) {
                        Console.Write(" (Gene or codon structure error.)");
                    }
                    Console.WriteLine();
                    break;

                case "13":
                    Console.Write("DNA strand   : ");
                    WriteDNA(dnaStrand1, geneLinePreference);

                    Operation13(dnaStrand1);
                    break;

                case "14":
                    Console.Write("DNA strand   : ");
                    WriteDNA(dnaStrand1, geneLinePreference);

                    Operation14(dnaStrand1);
                    break;

                case "15":
                    Console.Write("DNA strand   : ");
                    WriteDNA(dnaStrand1, geneLinePreference);

                    Console.Write("Number of nucleotides  : ");
                    int nucleotideCount = Convert.ToInt32(Console.ReadLine());

                    Operation15(dnaStrand1, nucleotideCount);
                    break;

                case "16":
                    Console.Write("DNA strand   : ");
                    WriteDNA(dnaStrand1, geneLinePreference);

                    Operation16(dnaStrand1);
                    break;

                case "17":
                    Operation17(dnaStrand1, dnaStrand2);
                    break;

                case "exit":
                    return;

                default:
                    break;
            }

            for (int i = 0; i < Console.BufferWidth - 1; i++) {
                Console.Write("─");
            }
            Console.WriteLine();
            GetInput();

        }

        geneLinePreference = 0;

        Console.WriteLine("Please type the number of the operation you want to do below.");
        Console.WriteLine("1    => Load a DNA sequence from a file");
        Console.WriteLine("2    => Load a DNA sequence from a string");
        Console.WriteLine("3    => Generate random DNA sequence of a BLOB");
        Console.WriteLine("4    => Check DNA gene structure");
        Console.WriteLine("5    => Check DNA of BLOB organism");
        Console.WriteLine("6    => Produce complement of a DNA sequence");
        Console.WriteLine("7    => Determine amino acids");
        Console.WriteLine("8    => Delete codons");
        Console.WriteLine("9    => Insert codons");
        Console.WriteLine("10   => Find codons");
        Console.WriteLine("11   => Reverse codons");
        Console.WriteLine("12   => Find the number of genes in a DNA strand (BLOB or not)");
        Console.WriteLine("13   => Find the shortest gene in a DNA strand ");
        Console.WriteLine("14   => Find the longest gene in a DNA strand");
        Console.WriteLine("15   => Find the most repeated n-nucleotide sequence in a DNA strand (STR)");
        Console.WriteLine("16   => Hydrogen bond statistics for a DNA strand");
        Console.WriteLine("17   => Simulate BLOB generations using DNA strand 1 and 2");
        Console.WriteLine("exit => Exits the program");
        for (int i = 0; i < Console.BufferWidth - 1; i++) {
            Console.Write("─");
        }
        Console.WriteLine("\n");

        GetInput();
    }
}