using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp2
{
    class DoubleLinkedList
    {

        class DoublyLinkedListNode
        {
            public int data;
            public DoublyLinkedListNode next;
            public DoublyLinkedListNode prev;
        }


        static DoublyLinkedListNode reverse(DoublyLinkedListNode head)
        {
            DoublyLinkedListNode temp = null;
            DoublyLinkedListNode newHead = null;
            DoublyLinkedListNode currentHead = head;

            while (currentHead != null)
            {
                temp = currentHead.prev;
                currentHead.prev = currentHead.next;
                currentHead.next = temp;
                newHead = currentHead;
                currentHead = currentHead.prev;
            }

            return newHead;
        }
    }
}
