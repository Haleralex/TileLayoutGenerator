My solution to a specific problem, for a Unity Developer position.

A tile layout generator needs to be written. The tiles
are laid in rows from bottom to top, from left to right with a given seam and offset.
The program must have a UI that allows you to set:
1) the size of the seam between the rows and the tiles in the rows 2) the size of the offset of the rows relative to each other 3) the angle of rotation of all rows around the center of the plane
If any of the parameters is changed, the program should immediately display the new result of paving and calculation.
The result of the program operation:
1) Visual display of tile layout
2) Display of the total area of visible parts of tiles m2
The size of the plane can be any size, but it must accommodate at least 4 rows of tiles and at least 4 tiles in each row. The texture of the tiles can be taken any tile, such as this one
https://kerama-marazzi.com/catalog/ceramic_tile/vt_a274_16000/
An important point is the fact that the tiles should not "stick out" beyond the wall.
Spend a lot of time on the UI layout, too, we should not evaluate it at this stage.
When solving the problem, it will be more convenient to define tiles using contours and after all manipulations triangulate them in Mesh.
You are free to use any libraries that are required.
The result is accepted as a link to the Unity project in github with the solution of the task.
The main task in this assignment is to calculate the area of tiles that were used to pave the plane, without taking into account the size of seams and cut triangles.
