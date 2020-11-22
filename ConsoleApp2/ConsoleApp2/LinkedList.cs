using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApp2
{
    public class Node
    {
        public int data;
        public Node next;
        public Node arbitary;

        public Node() : this(0)
        {
        }

        public Node(int x)
        {
            data = x;
            next = null;
            arbitary = null;
        }
    }
    public class LinkedList
    {
        public void Begin()
        {
            Node start = new Node(1)
            {
                next = new Node(2)
            };
            start.next.next = new Node(3)
            {
                next = new Node(4)
            };
            start.next.next.next.next = new Node(5);

            start.arbitary = start.next.next;

            // 2's random points to 1  
            start.next.arbitary = start;

            // 3's and 4's random points to 5  
            start.next.next.arbitary = start.next.next.next.next;
            start.next.next.next.arbitary = start.next.next.next.next;

            // 5's random points to 2  
            start.next.next.next.next.arbitary = start.next;

            Console.WriteLine("Original list : ");
            print(start);

            Console.WriteLine("");
            Console.WriteLine("Cloned list : ");
            Node cloned_list = CloneLinkedList(start);
            print(cloned_list);

        }

        private Node CloneLinkedList(Node start)
        {
            Node oldList = start;

            Node cloneList = new Node(oldList.data);
            Node currentCloneList = cloneList;

            oldList = oldList.next;
            while (oldList != null)
            {
                currentCloneList.next = new Node(oldList.data);
                currentCloneList = currentCloneList.next;
                oldList = oldList.next;
            }

            currentCloneList = cloneList;
            oldList = start;

            while (oldList != null && currentCloneList != null)
            {
                Node temp = oldList.next;
                currentCloneList.arbitary = oldList;
                oldList.next = currentCloneList;
                currentCloneList = currentCloneList.next;

                oldList = temp;
            }

            currentCloneList = cloneList;

            while (currentCloneList != null)
            {
                currentCloneList.arbitary = currentCloneList.arbitary.arbitary.next;
                currentCloneList = currentCloneList.next;
            }

            return cloneList;
        }

        void print(Node start)
        {
            Node ptr = start;
            while (ptr != null)
            {
                Console.WriteLine(string.Format("Data = {0}  Arbitary Data {1}", ptr.data, ptr.arbitary.data));
                //Console.WriteLine(string.Format("Data = {0}", ptr.data));
                ptr = ptr.next;
            }
        }
    }
}
