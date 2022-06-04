using Tuft.Dataplane;
using Tuft.Dataplane.NOS;
using Tuft.Properties;
using Tuft.ui;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Tuft.ExpermentsResults.Energy_consumptions
{
    class ResultsObject
    {
        public double AverageEnergyConsumption { get; set; }
        public double AverageHops { get; set; }
        public double AverageWaitingTime { get; set; }
        public double AverageRedundantTransmissions { get; set; }
        public double AverageRoutingDistance { get; set; }
        public double AverageTransmissionDistance { get; set; }
        public double AverageEnergyConsumedForControl { get; set; }
    }

    public class ValParPair
    {
        public string Par { get; set; }
        public string Val { get; set; }
    }

    /// <summary>
    /// Interaction logic for ExpReport.xaml
    /// </summary>
    public partial class ExpReport : Window
    {

        public ExpReport(MainWindow _mianWind)
        {
            InitializeComponent();

            List<ValParPair> List = new List<ValParPair>();
            ResultsObject res = new ResultsObject();
            

            double hopsCoun = 0;
            double routingDisEf = 0;
            double avergTransDist = 0;
            double AverageHops = 0;
            double AverageHopscontrol = 0;
            double AverageHopsData = 0;
            double AverageRoutingDistance = 0;

            foreach (Packet pk in PublicParameters.FinishedRoutedPackets)
            {
                hopsCoun += pk.Hops;
                routingDisEf += pk.RoutingDistanceEfficiency;
                avergTransDist += pk.AverageTransDistrancePerHop;

                AverageHops = pk.Hops / PublicParameters.OverallDelieverdPackets;
                AverageRoutingDistance = PublicParameters.TotalRoutingDistance / PublicParameters.OverallDelieverdPackets;
            }

            double NumberOfGenPackets = PublicParameters.OverallGeneratedPackets;// Convert.ToDouble(PublicParameters.NumberofGeneratedDataPackets) + Convert.ToDouble(PublicParameters.NumberofGeneratedQueryPackets);
            double NumberofDeliveredPacket = PublicParameters.OverallDelieverdPackets;// Convert.ToDouble(PublicParameters.NumberofDeliveredPackets);
            double succesRatio = PublicParameters.DeliveredRatio;

            res.AverageEnergyConsumption = PublicParameters.TotalEnergyConsumptionJoule;

            double averageWaitingTime = Convert.ToDouble(PublicParameters.TotalWaitingTime) / PublicParameters.OverallDelieverdPackets;
            res.AverageWaitingTime = averageWaitingTime;
            double avergaeRedundan = Convert.ToDouble(PublicParameters.TotalReduntantTransmission) / PublicParameters.OverallDelieverdPackets;
            res.AverageRedundantTransmissions = avergaeRedundan;
            res.AverageHops = hopsCoun / PublicParameters.OverallDelieverdPackets;
            res.AverageRoutingDistance = routingDisEf / PublicParameters.OverallDelieverdPackets;
            res.AverageTransmissionDistance = avergTransDist / PublicParameters.OverallDelieverdPackets;
            res.AverageEnergyConsumedForControl = PublicParameters.EnergyComsumedForControlPackets / (PublicParameters.NumberofGeneratedControlPackets);
            List.Add(new ValParPair() {Par="Number of Nodes", Val= _mianWind.myNetWork.Count.ToString() } );
            List.Add(new ValParPair() { Par = "Communication Range Radius", Val = PublicParameters.CommunicationRangeRadius.ToString()+" m"});
            //List.Add(new ValParPair() { Par = "Density", Val = PublicParameters.Density.ToString()});
            List.Add(new ValParPair() { Par = "Packet Rate", Val = _mianWind.PacketRate });
            List.Add(new ValParPair() { Par = "Simulation Time", Val = PublicParameters.SimulationTime.ToString() + " s" });
            List.Add(new ValParPair() { Par = "Start up time", Val = PublicParameters.MacStartUp.ToString() + " s" });
            List.Add(new ValParPair() { Par = "Active Time", Val = PublicParameters.Periods.ActivePeriod.ToString() + " s" });
            List.Add(new ValParPair() { Par = "Sleep Time", Val = PublicParameters.Periods.SleepPeriod.ToString() + " s" });
            //
            List.Add(new ValParPair() { Par = "Queue Time", Val = PublicParameters.QueueTime.Seconds.ToString() });
            List.Add(new ValParPair() { Par = "Lifetime (s)", Val = PublicParameters.SimulationTime.ToString() });
            List.Add(new ValParPair() { Par = "# Generated packet", Val = PublicParameters.OverallGeneratedPackets.ToString() });
            List.Add(new ValParPair() { Par = "# Delivered packet", Val = PublicParameters.OverallDelieverdPackets.ToString() });
            List.Add(new ValParPair() { Par = "# Droped packet", Val = PublicParameters.NumberofDropedPackets.ToString() });
            List.Add(new ValParPair() { Par = "Success %", Val = succesRatio.ToString() });
            List.Add(new ValParPair() { Par = "Dropped %", Val = (100 - succesRatio).ToString() });
            List.Add(new ValParPair() { Par = "# Delieverd Data packets", Val = PublicParameters.NumberOfDelieveredDataPackets.ToString() });
            List.Add(new ValParPair() { Par = "# Query & Control pck", Val = (PublicParameters.NumberofGeneratedControlPackets).ToString() });
            List.Add(new ValParPair() { Par = "Initial Energy (J)", Val = PublicParameters.BatteryIntialEnergy.ToString() });
            List.Add(new ValParPair() { Par = "Total Energy Consumption (J)", Val = res.AverageEnergyConsumption.ToString() });
            List.Add(new ValParPair() { Par = "Total Energy Consumption for Data Packets (J)", Val = PublicParameters.EnergyConsumedForDataPackets.ToString() });
            List.Add(new ValParPair() { Par = "Total Energy Consumption for Control Packets (J)", Val = PublicParameters.EnergyComsumedForControlPackets.ToString() });
            List.Add(new ValParPair() { Par = "Total Wasted Energy  (J)", Val = PublicParameters.TotalWastedEnergyJoule.ToString() });

            List.Add(new ValParPair() { Par = "Mean Value of Sink speed", Val = Settings.Default.SinkSpeed.ToString() });
            List.Add(new ValParPair() { Par = "Number of Nodes Dissemnating Data", Val = PublicParameters.NumberOfNodesDissemenating.ToString() });

            // Hops
            //List.Add(new ValParPair() { Par = "Average Hops", Val = AverageHops.ToString() });
            List.Add(new ValParPair() { Par = "Average Hops", Val = PublicParameters.AverageHops.ToString() });
            List.Add(new ValParPair() { Par = "Average Hops for Control", Val = PublicParameters.AverageHopscontrol.ToString() });
            List.Add(new ValParPair() { Par = "Average Hops for Data", Val = PublicParameters.AverageHopsData.ToString() });
            // Distances:
            // Distances:
            List.Add(new ValParPair() { Par = "Average Routing Distance (m)/path", Val = PublicParameters.AverageRoutingDistance.ToString() });
            List.Add(new ValParPair() { Par = "Average Transmission Distance (m)/hop", Val = PublicParameters.AverageTransmissionDistance.ToString() });

            List.Add(new ValParPair() { Par = "Average Delay in (s)/path", Val = PublicParameters.AverageDelay.ToString() });
            List.Add(new ValParPair() { Par = "Average Query Delay in (s)", Val = PublicParameters.AverageQueryDelay.ToString() }); ;
            //List.Add(new ValParPair() { Par = "Average Query delay TBDD/path", Val = res.AverageWaitingTime.ToString() });

            List.Add(new ValParPair() { Par = "Average Query & Control Energy Consumption (J)", Val = res.AverageEnergyConsumedForControl.ToString() });
            List.Add(new ValParPair() { Par = "Query & Control Energy Consumption Percentage(%)", Val = PublicParameters.ControlPacketsEnergyConsmPercentage.ToString() });


            


            // PublicParameters.NetworkName
            List.Add(new ValParPair() { Par = "Protocol", Val = "TBP" });
            List.Add(new ValParPair() { Par = "ATopology", Val = PublicParameters.NetworkName.ToString() });
            dg_data.ItemsSource = List;
        }
    }
}
