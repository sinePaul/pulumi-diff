# pulumi-diff
Overview of unwanted large pulumi diff.
The code used here never change between the operations
This example is using a storage account, a blob container and a blob but this behavior is happening on many resources

### 1. `pulumi up`

![image](https://github.com/sinePaul/pulumi-diff/assets/94847275/0408054f-3b67-4643-9320-0ce5c89ff2a0)

We can run any other `pulumi up` again wihtout having any diff 

### 2. `pulumi refresh`

The refresh command is importing lots of properties that cannot be edited from Pulumi
![image](https://github.com/sinePaul/pulumi-diff/assets/94847275/40ec5766-ce18-49b3-bd9c-c9ce4fa4dbd3)

![image](https://github.com/sinePaul/pulumi-diff/assets/94847275/02d2b580-8541-4586-94ae-2976ec57675b)


### 3. `pulumi up`

When trying to update the stack again, pulumi tries to delete the properties that are bought by the refresh 

![image](https://github.com/sinePaul/pulumi-diff/assets/94847275/a8d5644c-4ac2-4987-bbe0-4f989d15ab03)

![image](https://github.com/sinePaul/pulumi-diff/assets/94847275/eb2a9bae-76d2-4920-904d-af7d50027936)


#### Conclusion
When refreshing and updating our stack again, we will always have those properties in the diff. Those are optionals properties that we do not set in the code. 
We cannot use `IgnoreChanges` in `CustomResourceOptions` since the change is not coming from the code. 

We need to avoid unnecessary diffs to automate our workflow in CICD. 
In this example, we would like to be able to run this command without error `pulumi up -r --expect-no-changes -y`

#### From `pulumi about`

![image](https://github.com/sinePaul/pulumi-diff/assets/94847275/5b9fec9c-cbdd-405c-b503-e3ad65cf2e18)
