using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    // Make enum
    public enum Team
    {
        None,
        Scissor,
        Paper,
        Rock
    }
    
    public Sprite[] BodySprite;
    
    public Team _team;
    
    public float _speed;
    public float _rotationSpeed;
    
    public Transform _target;
    
    public float _viewDistance = 10f;
    
    public void SetTeam(Team team)
    {
        _team = team;
        GetComponent<SpriteRenderer>().sprite = BodySprite[(int)team];
        _target = null;
        switch (team)
        {
            case Team.Scissor:
                gameObject.tag = "Scissor";
                break;
            case Team.Paper:
                gameObject.tag = "Paper";
                break;
            case Team.Rock:
                gameObject.tag = "Rock";
                break;
        }
    }
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    
    void Update()
    {
        if (_target != null)
        {
            // Rotate towards target 2D
            Vector3 dir = _target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);

            // Move towards target 2D
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
            
            if(_target != null)
            {
                // Check if target and very close
                if (Vector2.Distance(transform.position, _target.position) < 0.5f)
                {
                    if (_team == Team.Paper)
                    {
                        _target.gameObject.GetComponent<AIController>().SetTeam(Team.Paper);
                        _target = null;
                    }
                    
                    if (_team == Team.Rock)
                    {
                        _target.gameObject.GetComponent<AIController>().SetTeam(Team.Rock);
                        _target = null;
                    }
                    
                    if (_team == Team.Scissor)
                    {
                        _target.gameObject.GetComponent<AIController>().SetTeam(Team.Scissor);
                        _target = null;
                    }
                }
            }
            
            if(_target != null)
            {
                // Check if target is changed Team
                if (_target.GetComponent<AIController>()._team == _team)
                {
                    _target = null;
                }
            }
        }
        
        if(_target == null)
        {
            // Create a circular raycast, target the nearest AI in 2D
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _viewDistance);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].GetComponent<AIController>() != null)
                {
                    // Check Team
                    if (hitColliders[i].transform.GetComponent<AIController>()._team != _team)
                    {
                        if (_team == Team.Paper)
                        {
                            if (hitColliders[i].transform.GetComponent<AIController>()._team == Team.Rock)
                            {
                                _target = hitColliders[i].transform;
                            }
                        }
                    
                        if (_team == Team.Rock)
                        {
                            if (hitColliders[i].transform.GetComponent<AIController>()._team == Team.Scissor)
                            {
                                _target = hitColliders[i].transform;
                            }
                        }
                    
                        if (_team == Team.Scissor)
                        {
                            if (hitColliders[i].transform.GetComponent<AIController>()._team == Team.Paper)
                            {
                                _target = hitColliders[i].transform;
                            }
                        }
                    }
                }
                i++;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (GameManager.instance.EnableCollisionAttack)
        {
            // if collide with other AI
            if (col.gameObject.GetComponent<AIController>())
            {
                if(col.gameObject.GetComponent<AIController>()._team != _team)
                {
                    if (_team == Team.Paper)
                    {
                        col.gameObject.GetComponent<AIController>().SetTeam(Team.Paper);
                        _target = null;
                    }
                    
                    if (_team == Team.Rock)
                    {
                        col.gameObject.GetComponent<AIController>().SetTeam(Team.Rock);
                        _target = null;
                    }
                    
                    if (_team == Team.Scissor)
                    {
                        col.gameObject.GetComponent<AIController>().SetTeam(Team.Scissor);
                        _target = null;
                    }
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewDistance);
    }
}
