public class SynchronizedIncrement implements Runnable{
    private static final int NUM_THREADS = 100;
    private static final int NUM_INCREMENTS = 1000000;
    private Counter counter;

    public SynchronizedIncrement(Counter counter){
        this.counter = counter;
    }
    public static void main(String[] args) throws InterruptedException{
        Thread[] threads = new Thread[NUM_THREADS];
        Counter counter = new Counter();
        for (int i = 0; i < NUM_THREADS; ++i){
            threads[i] = new Thread(new SynchronizedIncrement(counter));
            threads[i].start();
        }
        for (int i = 0; i < NUM_THREADS; ++i){
            threads[i].join();
        }
        System.out.println("total count = " + counter.getCount());
    }

    @Override
    public void run(){
        for (int i = 0; i < NUM_INCREMENTS; ++i){
            counter.incrementCount();
        }
    }
}
