public class Counter{
    private int count = 0;

    public int getCount(){
        return count ;
    }

    //public synchronized void incrementCount(){
    public void incrementCount(){
        ++count;
    }
}
