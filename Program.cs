using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


namespace saitama
{
  class Program
  {
    static void Main(string[] args)
    {
      uint b=Convert.ToUInt32(args[0]);
      uint[] facts=Factor.Get(b);
     Console.WriteLine($@"
         {b} の素因数は
         {String.Join(",",facts)}
     ");
    }
  }

  public class Factor{
    private static uint mask=0b_00000000_00000000_00000000_00000001;
    private List<uint> facts=new List<uint>(){};
    private List<uint> primeFacts=new List<uint>();

    private void correctFact(uint f){
      for(uint i=2;i<f;i++){
        if(isFactor(i)){
          facts.Add(i);
          if(facts.Count % 1000 == 0){
       //     Console.WriteLine($"+{facts.Count} = {i}");
          }
        }
      }
    }

    private Factor(uint f){
      correctFact(f);
      Console.WriteLine("素数リスト取得完了");
      //まず素数をコレクションする
      while(0<f){
        foreach(uint fact in facts){
          //余りが0以外だった場合は繰り返し
          if(f % fact !=0 )continue;
          uint diff=f/fact;

          f=diff;
          primeFacts.Add(fact);
          if(facts.Exists(a=>a==diff)){
            primeFacts.Add(diff);
            return;
          }
          break;
        }
      }
    }

    private bool isFactor(uint f){
      if(f==2)return true;
      //末尾ビットが0ならfalse
      if(0==(f & mask)){
        return false;
      }

      //既存の素数を検索して合致したらtrue
      //素数ではない可能性があるので、計算
      foreach(uint fact in facts){
        if(fact==f){
          return true;
        }

        if( f % fact==0){
          return false;
        }
      }
      return true;
    }

    public static uint[] Get(uint f){
      return new Factor(f);
    }

    public static implicit operator uint[](Factor f){
      return f.primeFacts.ToArray();
    }
  }
}
