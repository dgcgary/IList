using System.Diagnostics;

namespace IListUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        // M�todo auxiliar para crear una lista doblemente enlazada a partir de un array de valores
        private DoublyLinkedList CreateListOrder(params int[] values)
        {
            var list = new DoublyLinkedList();
            foreach (var value in values)
            {
                list.InsertInOrder(value);
            }
            return list;
        }

        // Prueba que verifica que se lanza una excepci�n cuando la lista A es nula
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSorted_NullListA()
        {
            var listB = CreateListOrder(9, 40, 50);
            DoublyLinkedList? listA = null;
            var dummyList = new DoublyLinkedList(); // Instancia v�lida para llamar al m�todo
            dummyList.MergeSorted(listA!, listB, SortDirection.Ascending);
        }

        // Prueba que verifica que se lanza una excepci�n cuando la lista B es nula
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestMergeSorted_NullListB()
        {
            var listA = CreateListOrder(10, 15);
            DoublyLinkedList? listB = null;
            var dummyList = new DoublyLinkedList(); // Instancia v�lida para llamar al m�todo
            dummyList.MergeSorted(listA, listB!, SortDirection.Ascending);
        }

        // Prueba que verifica la fusi�n de dos listas en orden descendente
        [TestMethod]
        public void TestMergeSorted_Descending()
        {
            var listA = CreateListOrder(10, 15);
            var listB = CreateListOrder(9, 40, 50);

            Debug.Print("ListA antes de MergeSorted: " + ListToString(listA));
            Debug.Print("ListB antes de MergeSorted: " + ListToString(listB));

            listA.MergeSorted(listA, listB, SortDirection.Descending);

            Debug.Print("ListA despu�s de MergeSorted: " + ListToString(listA));

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

        // Prueba que verifica la fusi�n de dos listas en orden ascendente
        [TestMethod]
        public void TestMergeSorted_Ascending()
        {
            var listA = CreateListOrder(4, 6, 8, 9, 46, 80, 81);
            var listB = CreateListOrder(1, 2, 7, 48, 90);

            Debug.Print("ListA antes de MergeSorted: " + ListToString(listA));
            Debug.Print("ListB antes de MergeSorted: " + ListToString(listB));

            listA.MergeSorted(listA, listB, SortDirection.Ascending);

            Debug.Print("ListA despu�s de MergeSorted: " + ListToString(listA));

            var expected = new List<int> { 1, 2, 4, 6, 7, 8, 9, 46, 48, 80, 81, 90 };
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

        // Prueba que verifica la fusi�n de una lista vac�a A con una lista B en orden descendente
        [TestMethod]
        public void TestMergeSorted_EmptyListA_Descending()
        {
            var listA = new DoublyLinkedList();
            var listB = CreateListOrder(9, 40, 50);

            Debug.Print("ListA antes de MergeSorted: " + ListToString(listA));
            Debug.Print("ListB antes de MergeSorted: " + ListToString(listB));

            listA.MergeSorted(listA, listB, SortDirection.Descending);

            Debug.Print("ListA despu�s de MergeSorted: " + ListToString(listA));

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

        // Prueba que verifica la fusi�n de una lista vac�a B con una lista A en orden ascendente
        [TestMethod]
        public void TestMergeSorted_EmptyListB_Ascending()
        {
            var listA = CreateListOrder(10, 15);
            var listB = new DoublyLinkedList();

            Debug.Print("ListA antes de MergeSorted: " + ListToString(listA));
            Debug.Print("ListB antes de MergeSorted: " + ListToString(listB));

            listA.MergeSorted(listA, listB, SortDirection.Ascending);

            Debug.Print("ListA despu�s de MergeSorted: " + ListToString(listA));

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

        // M�todo auxiliar para convertir una lista doblemente enlazada a una cadena de texto
        private string ListToString(DoublyLinkedList list)
        {
            var values = new List<int>();
            var current = list.head;
            while (current != null)
            {
                values.Add(current.Value);
                current = current.Next;
            }
            return string.Join(", ", values);
        }
    }

    [TestClass]
    public class UnitTest2
    {
        // Prueba que verifica que se lanza una excepci�n cuando la lista es nula
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestInvertirLista_NullList()
        {
            DoublyLinkedList? list = null;
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            list.InvertirLista();
        }

        // Prueba que verifica la inversi�n de una lista vac�a
        [TestMethod]
        public void TestInvertirLista_EmptyList()
        {
            var list = CreateList();
            Debug.Print("Lista antes de InvertirLista: " + ListToString(list));
            list.InvertirLista();
            Debug.Print("Lista despu�s de InvertirLista: " + ListToString(list));

            var expected = new List<int> { };
            var actual = new List<int>();

            while (true)
            {
                try
                {
                    actual.Add(list.DeleteFirst());
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        // Prueba que verifica la inversi�n de una lista no vac�a
        [TestMethod]
        public void TestInvertirLista_NonEmptyList()
        {
            var list = CreateList(1, 3, 5, 7, 9, 5, 4, 72);

            Debug.Print("Lista antes de InvertirLista: " + ListToString(list));
            list.InvertirLista();
            Debug.Print("Lista despu�s de InvertirLista: " + ListToString(list));

            var expected = new List<int> { 72, 4, 5, 9, 7, 5, 3, 1 };
            var actual = new List<int>();

            while (true)
            {
                try
                {
                    actual.Add(list.DeleteFirst());
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        // Prueba que verifica la inversi�n de una lista con un solo elemento
        [TestMethod]
        public void TestInvertirLista_SingleElementList()
        {
            var list = CreateList(2);

            Debug.Print("Lista antes de InvertirLista: " + ListToString(list));
            list.InvertirLista();
            Debug.Print("Lista despu�s de InvertirLista: " + ListToString(list));

            var expected = new List<int> { 2 };
            var actual = new List<int>();

            while (true)
            {
                try
                {
                    actual.Add(list.DeleteFirst());
                }
                catch (InvalidOperationException)
                {
                    break;
                }
            }

            CollectionAssert.AreEqual(expected, actual);
        }

        // M�todo auxiliar para crear una lista doblemente enlazada a partir de un array de valores en el mismo orden
        private DoublyLinkedList CreateList(params int[] values)
        {
            var list = new DoublyLinkedList();
            if (values.Length == 0)
            {
                return list;
            }

            list.head = new Node(values[0]);
            var current = list.head;
            for (int i = 1; i < values.Length; i++)
            {
                var newNode = new Node(values[i]);
                current.Next = newNode;
                newNode.Previous = current;
                current = newNode;
            }

            return list;
        }

        // M�todo auxiliar para convertir una lista doblemente enlazada a una cadena de texto
        private string ListToString(DoublyLinkedList list)
        {
            var values = new List<int>();
            var current = list.head;
            while (current != null)
            {
                values.Add(current.Value);
                current = current.Next;
            }
            return string.Join(", ", values);
        }
    }

    [TestClass]
    public class UnitTest3
    {
        // Prueba que verifica que se lanza una excepci�n cuando se intenta obtener el elemento medio de una lista vac�a
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetMiddle_NullList()
        {
            var list = new DoublyLinkedList();
            list.GetMiddle();
        }

        // Prueba que verifica que se lanza una excepci�n cuando se intenta obtener el elemento medio de una lista vac�a
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestGetMiddle_EmptyList()
        {
            var list = new DoublyLinkedList();
            list.GetMiddle();
        }

        // Prueba que verifica la obtenci�n del elemento medio de una lista con un solo elemento
        [TestMethod]
        public void TestGetMiddle_SingleElement()
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(1);
            Assert.AreEqual(1, list.GetMiddle());
        }

        // Prueba que verifica la obtenci�n del elemento medio de una lista con dos elementos
        [TestMethod]
        public void TestGetMiddle_TwoElements()
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(1);
            Debug.WriteLine($"After inserting 1: Middle = {list.GetMiddle()}");
            list.InsertInOrder(4);
            Debug.WriteLine($"After inserting 2: Middle = {list.GetMiddle()}");
            Assert.AreEqual(4, list.GetMiddle());
        }

        // Prueba que verifica la obtenci�n del elemento medio de una lista con un n�mero impar de elementos
        [TestMethod]
        public void TestGetMiddle_OddNumberOfElements()
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(0);
            Debug.WriteLine($"After inserting 0: Middle = {list.GetMiddle()}");
            list.InsertInOrder(2);
            Debug.WriteLine($"After inserting 2: Middle = {list.GetMiddle()}");
            list.InsertInOrder(3);
            Debug.WriteLine($"After inserting 3: Middle = {list.GetMiddle()}");
            list.InsertInOrder(6);
            Debug.WriteLine($"After inserting 6: Middle = {list.GetMiddle()}");
            list.InsertInOrder(9);
            Debug.WriteLine($"After inserting 9: Middle = {list.GetMiddle()}");
            list.InsertInOrder(17);
            Debug.WriteLine($"After inserting 17: Middle = {list.GetMiddle()}");
            Assert.AreEqual(6, list.GetMiddle());
        }

        // Prueba que verifica la obtenci�n del elemento medio de una lista con un n�mero par de elementos
        [TestMethod]
        public void TestGetMiddle_EvenNumberOfElements()
        {
            var list = new DoublyLinkedList();
            list.InsertInOrder(7);
            Debug.WriteLine($"After inserting 7: Middle = {list.GetMiddle()}");
            list.InsertInOrder(9);
            Debug.WriteLine($"After inserting 9: Middle = {list.GetMiddle()}");
            list.InsertInOrder(10);
            Debug.WriteLine($"After inserting 10: Middle = {list.GetMiddle()}");
            list.InsertInOrder(3);
            Debug.WriteLine($"After inserting 3: Middle = {list.GetMiddle()}");
            list.InsertInOrder(6);
            Debug.WriteLine($"After inserting 6: Middle = {list.GetMiddle()}");
            list.InsertInOrder(19);
            Debug.WriteLine($"After inserting 19: Middle = {list.GetMiddle()}");
            list.InsertInOrder(1);
            Debug.WriteLine($"After inserting 2: Middle = {list.GetMiddle()}");
            list.InsertInOrder(13);
            Debug.WriteLine($"After inserting 13: Middle = {list.GetMiddle()}");
            Assert.AreEqual(9, list.GetMiddle());
            //lista que se genera: (1, 3, 6, 7, 9, 10, 13, 19)
        }
    }
}
