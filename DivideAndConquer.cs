using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class DivideAndConquer : Algorithm
    {
        public List<Point> sort_points(List<Point> input)
        {
            return input.OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
        }
        public int get_top(List<Point> points, bool left)
        {
            int index = 0;

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y > points[index].Y)
                    index = i;
                else if (points[i].Y == points[index].Y)
                {
                    if (points[i].X < 0)
                    {
                        if (points[i].X > points[index].X)
                            index = i;
                    }
                    else if (points[i].X > 0)
                    {
                        if (left)
                        {
                            if (points[i].X < points[index].X)
                                index = i;
                        }
                        else
                        {
                            if (points[i].X > points[index].X)
                                index = i;
                        }
                        

                    }

                }

            }

            return index;
        }

        public int get_lower(List<Point> points)
        {
            int index = 0;

            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y < points[index].Y)
                    index = i;
                else if (points[i].Y == points[index].Y)
                {
                   
                        if (points[i].X > points[index].X)
                            index = i;
                }

            }

            return index;
        }

        
        
       
        public List<Point> Merge_subsets(List<Point> left_sub, List<Point> right_sub)
        {

            int left_size = left_sub.Count;
            int right_size = right_sub.Count;


            int  upper_left = get_top(left_sub, true);
            int  upper_right = get_top(right_sub, false);

            int lower_left = get_lower(left_sub);
            int lower_right = get_lower(right_sub);



            List<Point> result_points = new List<Point>();


            int ind = upper_left;



            if (!result_points.Contains(left_sub[upper_left]))
                result_points.Add(left_sub[upper_left]);


            while (ind != lower_left)
            {
                ind = (ind + 1) % left_size;


                if (!result_points.Contains(left_sub[ind]))
                {
                    result_points.Add(left_sub[ind]);

                }


            }

            ind = lower_right;
            if (!result_points.Contains(right_sub[lower_right]))

                result_points.Add(right_sub[lower_right]);

            while (ind != upper_right)
            {
                ind = (ind + 1) % right_size;

                if (!result_points.Contains(right_sub[ind]))

                    result_points.Add(right_sub[ind]);

            }


            return result_points;
        }

        public List<Point> Divide_points(List<Point> input_points)
        {

            List<Point> LEFT_POINT = new List<Point>();
            List<Point> RIGHT_POINT = new List<Point>();


            // STOP CONDATION

            if (input_points.Count == 1)
                return input_points;



            //Divide PART 
            int input_points_size = input_points.Count;

            int i = 0;

            while (i < input_points_size / 2)
            {
                LEFT_POINT.Add(input_points[i]);

                i += 1;
            }

            i = input_points_size / 2;

            while (i < input_points_size)
            {
                RIGHT_POINT.Add(input_points[i]);

                i += 1;
            }


            //RECURSION PART 
            List<Point> Res_left = Divide_points(LEFT_POINT);
            List<Point> Res_right = Divide_points(RIGHT_POINT);

            //Merge Part

            return Merge_subsets(Res_left, Res_right);


        }
        static IEnumerable<List<T>> Subsets<T>(List<T> objects, int maxLength)
        {
            if (objects == null || maxLength <= 0)
                yield break;
            var stack = new Stack<int>(maxLength);
            int i = 0;
            while (stack.Count > 0 || i < objects.Count)
            {
                if (i < objects.Count)
                {
                    if (stack.Count == maxLength)
                        i = stack.Pop() + 1;
                    stack.Push(i++);
                    yield return (from index in stack.Reverse()
                                  select objects[index]).ToList();
                }
                else
                {
                    i = stack.Pop() + 1;
                    if (stack.Count > 0)
                        i = stack.Pop() + 1;
                }
            }
        }
        public bool get_segment(Point p1, Point p2, Point p3)
        {

            return (CGUtilities.HelperMethods.PointOnSegment(p3, p1, p2));

        }

        public void get_segment_items(List<Point> comb, ref List<Point> co)
        {

            if (get_segment(comb[0], comb[1], comb[2]))
            {
                if (!co.Contains(comb[2]))
                {
                    co.Add(comb[2]);
                }
            }

            if (get_segment(comb[1], comb[2], comb[0]))
            {
                if (!co.Contains(comb[0]))
                {
                    co.Add(comb[0]);
                }
            }

            if (get_segment(comb[0], comb[2], comb[1]))
            {
                if (!co.Contains(comb[1]))
                {
                    co.Add(comb[1]);
                }
            }


        }

        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            List<Point> input_points = new List<Point>();

            for (int i = 0; i < points.Count; i++)
            {
                if (!input_points.Contains(points[i]))
                {
                    input_points.Add(points[i]);
                }
            }

            input_points = sort_points(input_points);


            outPoints = new List<Point>();
            List<Point> outpoint = Divide_points(input_points);


            for (int i = 0; i < outpoint.Count; i++)
            {
                Console.WriteLine(outpoint[i].X);
                Console.WriteLine(outpoint[i].Y);
                Console.WriteLine("*****************");
            }

            Console.WriteLine("////////////////");


            List<List<Point>> p = Subsets(outpoint, 3).ToList();


            List<Point> co = new List<Point>();


            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].Count == 3)
                {
                    get_segment_items(p[i], ref co);
                }

            }

            for (int i = 0; i < outpoint.Count; i++)
            {
                if (!co.Contains(outpoint[i]))
                {
                    outPoints.Add(outpoint[i]);
                }
            }



        }

        public override string ToString()
        {
            return "Convex Hull - Divide & Conquer";
        }

    }
}
