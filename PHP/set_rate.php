

<?php
$pdo_user = new PDO("sqlite:../Database/test_data.sqlite");

$rate = $_POST["rate"]; //Unityからユーザ名を取得
$group_root = $_POST["group"];
$sex_root = $_POST["sex"];
$age = $_POST["age"];
$dexterity_root = $_POST["dexterity"];

if($group_root == 0)
{
  $group = "A";
}
else if($group_root == 1)
{
  $group = "B";
}

if($sex_root == 0)
{
  $sex = "male";
}
else if($sex_root == 1)
{
  $sex = "female";
}

if($dexterity_root == 0)
{
  $dexterity = "left-handed";
}
else if($dexterity_root == 1)
{
  $dexterity = "right-handed";
}

try{
    $stmt = $pdo_user->query("INSERT INTO static(rate, category, sex, age, dexterity) VALUES(" . $rate . ",'" . $category . "','" . $sex . "'," . $age . ",'" . $dexterity . "')");
}catch(PDOException $e){
    var_dump($e->getMessage());
}
$pdo_user = null; //データベースとの通信切断

 ?>


?>
