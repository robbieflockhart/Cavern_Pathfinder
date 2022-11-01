using System;
using System.Collections.Generic;

namespace Caveroute
{
    class Caveroute
    {
        static void Main(string[] args)
        {

            String fileName = "";
            if(args.Length == 0)
            {
                fileName = "generated2000-1";
            }
            else
            {
                fileName = args[0];
            }

            String text = System.IO.File.ReadAllText(fileName + ".cav"); 
            String[] bits = text.Split(',');
            int noOfCaverns = int.Parse(bits[0]);
            int startOfMatrix = (noOfCaverns * 2 + 1);
            int[,] connectionMatrix = new int[noOfCaverns, noOfCaverns];
         
            Cavern goalCavern = new Cavern(noOfCaverns, int.Parse(bits[noOfCaverns * 2 - 1]), int.Parse(bits[noOfCaverns * 2]));
            Cavern startCavern = new Cavern(1, int.Parse(bits[1]), int.Parse(bits[2]));
            startCavern.setG(0);
            startCavern.setH(startCavern.CalculateH(startCavern, goalCavern));
            startCavern.setF(0, startCavern.getH());
            
            for(int i = 0; i < noOfCaverns; i++)
            {
                for(int j = 0; j <noOfCaverns; j++)
                {
                    connectionMatrix[i, j] = int.Parse(bits[startOfMatrix]);
                    startOfMatrix += 1;
                } 
            }

            List<Cavern> openList = new List<Cavern>();
            List<Cavern> closedList = new List<Cavern>();

            openList.Add(startCavern);

            while (openList.Count > 0)
            {
                Cavern currentCavern = openList[0];
                openList.RemoveAt(0);        

                if (currentCavern.getID() == goalCavern.getID())
                {
                    List<int> shortestPath = new List<int>();
                    Cavern cave = currentCavern;
                    while(true)
                    {
                        shortestPath.Add(cave.getID());
                        if( cave.getParent() == null)
                        {                          
                            break;
                        }
                        cave = cave.getParent();
                    }

                    shortestPath.Reverse();
                    string path = "";
                    foreach(int p in shortestPath)
                    {
                        path = path + Convert.ToString(p) + " ";
                    }

                    System.IO.File.WriteAllText(fileName + ".csn", path);
                    break;
                }

                int caveID = 1;
                for (int i = 0; i < noOfCaverns; i++)
                {
                    if (connectionMatrix[i, currentCavern.getID() - 1] == 1)
                    {
                        int caveX = int.Parse(bits[caveID * 2 - 1]);
                        int caveY = int.Parse(bits[caveID * 2]);
                        Cavern childCavern = new Cavern(caveID, caveX, caveY);
                        double caveG = currentCavern.CalculateG(currentCavern, childCavern, currentCavern.getG());
                        childCavern.setG(caveG);
                        double caveH = childCavern.CalculateH(childCavern, goalCavern);
                        childCavern.setH(caveH);
                        childCavern.setF(caveG, caveH);
                        childCavern.setParent(currentCavern);

                        bool flag = false;
                        for (int j = 0; j < openList.Count; j++)
                        {
                            if (openList[j].getID() == childCavern.getID())
                            {
                                if(openList[j].getF() < childCavern.getF())
                                {
                                    flag = true;
                                    break;
                                }
                                else if(openList[j].getF() >= childCavern.getF())
                                {
                                    openList.Remove(openList[j]);
                                    break;
                                }                        
                            }                                 
                        }

                        if(flag)
                        {
                            caveID += 1;
                            continue;
                        }

                        for (int j = 0; j < closedList.Count; j++)
                        {
                            if (closedList[j].getID() == childCavern.getID() && closedList[j].getF() <= childCavern.getF())
                            {
                                flag = true;
                                break;
                            }
                        }

                        if (flag == true)
                        {
                            caveID += 1;
                            continue;
                        }

                        if (openList.Count == 0)
                        {
                            openList.Add(childCavern);
                        }
                        else
                        {
                            for (int j = 0; j < openList.Count; j++)
                            {
                                if (openList[j].getF() < childCavern.getF())
                                {
                                    continue;
                                }
                                else
                                {
                                    openList.Insert(j, childCavern);
                                    break;
                                }
                            }
                            if (childCavern.getF() > openList[openList.Count - 1].getF())
                            {
                                openList.Add(childCavern);
                            }
                        } 
                    }

                    caveID += 1;
                }

                closedList.Add(currentCavern);
            }

            if(openList.Count == 0)
            {
                string noPath = "0";
                System.IO.File.WriteAllText(fileName + ".csn", noPath);
            }
        }
    }
}
