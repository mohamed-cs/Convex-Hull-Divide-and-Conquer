# Convex-Hull-Divide-and-Conquer
![image](https://user-images.githubusercontent.com/58373612/214441040-f78abf65-7283-4c0e-a172-9bd9e4c5f1d8.png)

<h1>Divide and conquer algorithm convex hull</h1>

<p>The key idea is that is we have two convex hull then, they can be merged in linear time to get a convex hull of a larger set of points. Divide and conquer algorithms solve problems by dividing them into smaller instances, solving each instance recursively and merging the corresponding results to a complete solution.</p>


<h1>Solution:</h1>
<p>Pre-requisite: Tangents between two convex polygons Algorithm: Given the set of points for which we have to find the convex hull. Suppose we know the convex hull of the left half points and the right half points, then the problem now is to merge these two convex hulls and determine the convex hull for the complete set. This can be done by finding the upper and lower tangent to the right and left convex hulls. This is illustrated here Tangents between two convex polygons Let the left convex hull be a and the right convex hull be b. Then the lower and upper tangents are named as 1 and 2 respectively, as shown in the figure. Then the red outline shows the final convex hull.</p>
! [image](https://user-images.githubusercontent.com/58373612/214441421-116c50c4-415b-46af-b2eb-4c34fa86b38e.png)

I create the tangent lines with top right , top left ,bottom right and bottom left points. 
