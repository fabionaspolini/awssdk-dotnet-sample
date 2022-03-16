using static System.Console;
// See https://aka.ms/new-console-template for more information

WriteLine(".:: AWS SDK ::.");

// var s3Client = new Amazon.S3.AmazonS3Client();
// var buckets = await s3Client.ListBucketsAsync();
// foreach (var item in buckets.Buckets)
//     WriteLine($"{item.BucketName}");

var appStreamclient = new Amazon.AppStream.AmazonAppStreamClient();
var fleets = await appStreamclient.DescribeFleetsAsync(new());

WriteLine($"| {"Name",-30} | {"InUse",10} | {"Available",10} | {"Desired",10} | {"Running",10} |");
foreach (var fleet in fleets.Fleets)
{
    WriteLine(
        $"| {fleet.Name,-30} " +
        $"| {fleet.ComputeCapacityStatus.InUse,10} " +
        $"| {fleet.ComputeCapacityStatus.Available,10} " +
        $"| {fleet.ComputeCapacityStatus.Desired,10} " +
        $"| {fleet.ComputeCapacityStatus.Running,10} |");
}

WriteLine("Fim");