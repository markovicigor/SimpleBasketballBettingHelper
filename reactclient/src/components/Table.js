import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import { getPercents} from '../services/list.js'
import '../utilities/Style.css'
import MyImage from '../items/logo_maxbet.png'
export default function MyTable() {
  
    const[percents, setPercents] = useState([]);
  

    
    useEffect(() => {
      let mounted = true;
      getPercents()
        .then(items => {
          if(mounted) {
           
            setPercents(items)
           console.log(items)
          }
        })
      return () => mounted = false;
    }, [])
    
    return (
      
      
      <div className="table-wrapper">
      <table className="fl-table"  striped="columns">
      <thead>
        <tr>
          <th>Home team</th>
          <th>Away team</th>
          <th>Betting site</th>
          <th>Points limit</th>
          <th>+</th>
          <th>-</th>
          <th>Chance for over </th>
          <th>Chance for under </th>
          
          
        </tr>
      </thead>
      <tbody>
      
      {Object.entries(percents)
      .map( ([key, value]) => 
                        
                       value.betGame.homeTeam !== null  ? (
                          <tr key={value.betGame.homeTeam}>
                           
                            <td>{value.betGame.homeTeam } </td>
                            <td>{value.betGame.awayTeam}</td>
                            <td><a href="https://www.maxbet.rs/ibet-web-client/#/home/leaguesWithMatches"><img width="20%" height="20%"
                              src={MyImage}
                              alt="MaxBet"
                             
                            /></a></td>
                            <td>{value.betGame.limit}</td>
                            <td>{value.betGame.over}</td>
                            <td>{value.betGame.under}</td>
                            <td>{Number((100-value.percentBoth).toFixed(2))}%</td>
                            <td>{Number((value.percentBoth).toFixed(2))}%</td>
                        </tr>
                        ) : null
                      
                     
                    )}
                    
      </tbody>
    </table>
      </div>
   
          
    );
  }
