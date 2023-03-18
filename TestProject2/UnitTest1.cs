using SF2022User_NN_Lib;


namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(16,50,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new(8, 0, 0);
            TimeSpan endWorkingTime = new(18, 0, 0);
            int consultationTime = 30;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            string[] ss = new string[]
            {
            "08:00-08:30",
            "08:30-09:00",
            "09:00-09:30",
            "09:30-10:00",
            "11:30-12:00",
            "12:00-12:30",
            "12:30-13:00",
            "13:00-13:30",
            "13:30-14:00",
            "14:00-14:30",
            "14:30-15:00",
            "15:40-16:10",
            "16:10-16:40",
            "17:30-18:00"
            };
            string my = "", dll = "";
            foreach (string? item in ss)
            {
                my += item;
            }
            foreach (string? item in s)
            {
                dll += item;
            }
            Assert.AreEqual(my, dll);
        }

        [TestMethod]
        public void TestMethod2()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(16,50,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new(8, 0, 0);
            TimeSpan endWorkingTime = new(18, 0, 0);
            int consultationTime = 30;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void TestMethod3()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(16,50,0)};
            int[] durations = new int[] { 60, 30 };
            TimeSpan beginWorkingTime = new(8, 0, 0);
            TimeSpan endWorkingTime = new(18, 0, 0);
            int consultationTime = 30;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void TestMethod4()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,10,0),
            new TimeSpan(15,30,0),
            new TimeSpan(17,30,0)};
            int[] durations = new int[] { 60, 15, 10, 15, 40 };
            TimeSpan beginWorkingTime = new(8, 0, 0);
            TimeSpan endWorkingTime = new(18, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsTrue(s.Length == 22);
        }

        [TestMethod]
        public void TestMethod5()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(6,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,10,0),
            new TimeSpan(15,30,0),
            new TimeSpan(18,30,0)};
            int[] durations = new int[] { 60, 15, 10, 15, 40 };
            TimeSpan beginWorkingTime = new(8, 0, 0);
            TimeSpan endWorkingTime = new(18, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime); Assert.IsTrue(s.Length == 26);
        }

        [TestMethod]
        public void TestMethod6()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(6,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(18,30,0)};
            int[] durations = new int[] { 60, 15, 10, 15, 40 };
            TimeSpan beginWorkingTime = new(8, 0, 0);
            TimeSpan endWorkingTime = new(18, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsInstanceOfType(s, typeof(string[]));
        }


        [TestMethod]
        public void TestMethod7()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,0,0),
            new TimeSpan(15,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(16,50,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new(18, 0, 0);
            TimeSpan endWorkingTime = new(8, 0, 0);
            int consultationTime = 30;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNull(s);
        }

        [TestMethod]
        public void TestMethod8()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(17,40,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new(10, 0, 0);
            TimeSpan endWorkingTime = new(19, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void TestMethod9()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(15,30,0),
            new TimeSpan(24,40,0)};
            int[] durations = new int[] { 60, 30, 10, 10, 40 };
            TimeSpan beginWorkingTime = new(24, 0, 0);
            TimeSpan endWorkingTime = new(26, 0, 0);
            int consultationTime = 20;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void TestMethod10()
        {
            TimeSpan[] startTimes = new TimeSpan[] { new TimeSpan(10,0,0),
            new TimeSpan(11,39,0),
            new TimeSpan(18,40,0)};
            int[] durations = new int[] { 11, 16, 7, 10, 40 };
            TimeSpan beginWorkingTime = new(10, 0, 0);
            TimeSpan endWorkingTime = new(20, 0, 0);
            int consultationTime = 14;
            string[] s = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);

            Assert.IsNotNull(s);
        }
    }
}