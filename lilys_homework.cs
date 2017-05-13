using System;
using System.Linq;
using System.Collections.Generic;

public class Program {
    public static void Main(string[] args) {
        Console.ReadLine();
        List<long> nums = Console.ReadLine().Split(' ')
            .Select<string, long>(s => long.Parse(s)).ToList();
        Dictionary<long, int> placements = getCorrectPlacements(nums);
        Dictionary<long, int> placementsReverse = getCorrectPlacementsReverse(nums);
        List<long> nums2 = nums.Select<long, long>(n => n).ToList();

        // Console.WriteLine("swaps:");
        int swaps = getNumSwaps(nums, placements);
        // Console.WriteLine("swapsReverse:");
        int swapsReverse = getNumSwaps(nums2, placementsReverse);
        Console.WriteLine(Math.Min(swaps, swapsReverse));
    }

    private static int getNumSwaps(List<long> nums, Dictionary<long, int> placements) {
        Dictionary<long, int> indices = getIndexMapping(nums);
        List<long> nums2 = nums.Select<long, long>(n => n).ToList();
        int swaps = 0;
        foreach (long n in nums2) {
            int correctIndex = placements[n];
            if (correctIndex != indices[n]) {
                // Console.WriteLine("index of {0} is {1} but should be {2}, swapping", n, indices[n], correctIndex);
                swap(nums, indices[n], correctIndex, indices);
                swaps++;
            }
        }



        // for (int i = 0; i < nums.Count; i++) {
        //     int correctIndex = placements[nums2[i]];
        //     // Console.WriteLine("curr index is {0} and correct index of {1} is {2}", i, nums[i], correctIndex);
        //     if (correctIndex != i) {
        //         swap(nums, i, correctIndex);//, indices);
        //         swaps++;
        //     }
        // }

        return swaps;
    }

    private static Dictionary<long, int> getIndexMapping(List<long> nums) {
        Dictionary<long, int> indices = new Dictionary<long, int>();
        for (int i = 0; i < nums.Count; i++) {
            indices[nums[i]] = i;
        }
        return indices;
    }

    private static Dictionary<long, int> getCorrectPlacements(List<long> nums) {
        List<long> sortedNums = nums.OrderBy<long, long>(n => n).ToList();
        Dictionary<long, int> placements = new Dictionary<long, int>();
        for (int i = 0; i < sortedNums.Count; i++) {
            placements[sortedNums[i]] = i;
        }
        return placements;
    }

    private static Dictionary<long, int> getCorrectPlacementsReverse(List<long> nums) {
        List<long> sortedNums = nums.OrderByDescending<long, long>(n => n).ToList();
        Dictionary<long, int> placements = new Dictionary<long, int>();
        for (int i = 0; i < sortedNums.Count; i++) {
            placements[sortedNums[i]] = i;
        }
        return placements;
    }

    private static void swap(List<long> nums, int a, int b, Dictionary<long, int> indices) {
        // Console.WriteLine("swapping {0} and {1}", nums[a], nums[b]);
        long tempA = nums[a];
        nums[a] = nums[b];
        nums[b] = tempA;
        indices[nums[a]] = a;
        indices[nums[b]] = b;
    }
}
