using static System.Console;
// See https://aka.ms/new-console-template for more information

WriteLine(".:: AWS SDK ::.");

// var s3Client = new Amazon.S3.AmazonS3Client();
// var buckets = await s3Client.ListBucketsAsync();
// foreach (var item in buckets.Buckets)
//     WriteLine($"{item.BucketName}");

var appStreamclient = new Amazon.AppStream.AmazonAppStreamClient();

while (true)
{
    Clear();
    WriteLine(DateTime.Now);
    var fleets = await appStreamclient.DescribeFleetsAsync(new());
    WriteLine($"| {"Name",-30} | {"InUse",10} | {"Available",10} | {"Pending",10} | {"Desired",10} | {"Running",10} | {"Utilization",10}");
    foreach (var fleet in fleets.Fleets)
    {
        WriteLine(
            $"| {fleet.Name,-30} " +
            $"| {fleet.ComputeCapacityStatus.InUse,10} " +
            $"| {fleet.ComputeCapacityStatus.Available,10} " +
            $"| {fleet.ComputeCapacityStatus.Desired - fleet.ComputeCapacityStatus.Running,10} " + // Pending
            $"| {fleet.ComputeCapacityStatus.Desired,10} " +
            $"| {fleet.ComputeCapacityStatus.Running,10} " + // ActualCapacity
            $"| {GetCapactityUtilization(fleet.ComputeCapacityStatus.Running, fleet.ComputeCapacityStatus.InUse),10:N2}");
    }
    Thread.Sleep(2000);
}

decimal GetCapactityUtilization(int running, int inUse) => running > 0 ? inUse * 100 / running : 100;

WriteLine("Fim");