#interface: GUSEK
#solver: glpsol

set N1 := 1..3;
set N2 := 1..3;

param c1 {N1}; #Coefficients of x
param c2 {N2}; #Coefficients of y
param l {N1};  #lower bounds

var x {N1} binary;	#fosse stato tra 0 e 1 il valore ottimo peggiorava
var y {N2} >=0;

minimize z: sum {i in N1} c1[i]*x[i] + sum {i in N2} c2[i]*y[i];
subject to Constr1: y[1] + 3*[y2] >= 15;
subject to Constr2: y[1] + 2*[y3] >= 10;
subject to Constr3: 2*y[1] + [y2] >= 20;

#oppure
#subject to Constr1: 45*x[1] + y[1] + 3*[y2] >= 60;
#subject to Constr2: 5*x[2] + y[1] + 2*[y3] >= 15;
#subject to Constr3: 10*x[2] + 2*y[1] + [y2] >= 30;	e così va meglio. 79.1, con x[3] = 0.1

subject to xy {i in N1}:
	y[i] <= l[i] * x[i];
	
data;
param c1 :=
1 24
2 12
3 16;

param c2 :=
1 4
2 2
3 3;

param l :=
1 15
2 10
3 20;

