namespace IListUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSorted_NullListA()
        {
            var listB = new DoublyLinkedList();
            listB.InsertInOrder(9);
            listB.InsertInOrder(40);
            listB.InsertInOrder(50);

            var listA = (DoublyLinkedList)null;
            var dummyList = new DoublyLinkedList(); // Instancia válida para llamar al método
            dummyList.MergeSorted(listA, listB, SortDirection.Ascending);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSorted_NullListB()
        {
            var listA = new DoublyLinkedList();
            listA.InsertInOrder(10);
            listA.InsertInOrder(15);

            var listB = (DoublyLinkedList)null;
            var dummyList = new DoublyLinkedList(); // Instancia válida para llamar al método
            dummyList.MergeSorted(listA, listB, SortDirection.Ascending);
        }

        [TestMethod]
        public void TestMergeSorted_Descending()
        {
            var listA = new DoublyLinkedList();
            listA.InsertInOrder(10);
            listA.InsertInOrder(15);

            var listB = new DoublyLinkedList();
            listB.InsertInOrder(9);
            listB.InsertInOrder(40);
            listB.InsertInOrder(50);

            listA.MergeSorted(listA, listB, SortDirection.Descending);

            var expected = new List<int> { 50, 40, 15, 10, 9 };
            var actual = new List<int>();

            while (true)
            {
                try
                {
                    actual.Add(listA.DeleteFirst());
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMergeSorted_Ascending()
        {
            var listA = new DoublyLinkedList();
            listA.InsertInOrder(0);
            listA.InsertInOrder(2);
            listA.InsertInOrder(6);
            listA.InsertInOrder(10);
            listA.InsertInOrder(25);

            var listB = new DoublyLinkedList();
            listB.InsertInOrder(3);
            listB.InsertInOrder(7);
            listB.InsertInOrder(11);
            listB.InsertInOrder(40);
            listB.InsertInOrder(50);

            listA.MergeSorted(listA, listB, SortDirection.Ascending);

            var expected = new List<int> { 0, 2, 3, 6, 7, 10, 11, 25, 40, 50 };
            var actual = new List<int>();

            while (true)
            {
                try
                {
                    actual.Add(listA.DeleteFirst());
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMergeSorted_EmptyListA_Descending()
        {
            var listA = new DoublyLinkedList();

            var listB = new DoublyLinkedList();
            listB.InsertInOrder(9);
            listB.InsertInOrder(40);
            listB.InsertInOrder(50);

            listA.MergeSorted(listA, listB, SortDirection.Descending);

            var expected = new List<int> { 50, 40, 9 };
            var actual = new List<int>();

            while (true)
            {
                try
                {
                    actual.Add(listA.DeleteFirst());
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMergeSorted_EmptyListB_Ascending()
        {
            var listA = new DoublyLinkedList();
            listA.InsertInOrder(10);
            listA.InsertInOrder(15);

            var listB = new DoublyLinkedList();

            listA.MergeSorted(listA, listB, SortDirection.Ascending);

            var expected = new List<int> { 10, 15 };
            var actual = new List<int>();

            while (true)
            {
                try
                {
                    actual.Add(listA.DeleteFirst());
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }

            CollectionAssert.AreEqual(expected, actual);
        }
    }



}